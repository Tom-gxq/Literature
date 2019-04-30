using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class AccountReportDatabase : IReportDatabase
    {
        private readonly AccountRepository _repository;
        private readonly AccessTokenRepository _tokenRepository;
        private readonly AccountInfoRepository _infoRepository;
        private readonly EventRepository _eventRepository;
        private readonly OpenIdRepository _openIdRepository;
        public AccountReportDatabase(AccountRepository repository, AccessTokenRepository tokenRepository, AccountInfoRepository infoRepository, 
            EventRepository eventRepository, OpenIdRepository openIdRepository)
        {
            _repository = repository;
            _tokenRepository = tokenRepository;
            _infoRepository = infoRepository;
            _eventRepository = eventRepository;
            _openIdRepository = openIdRepository;
        }

        public bool Add(AccountEntity item)
        {
            return _repository.AddAccount(item);
        }
        public bool Update(AccountEntity item)
        {
            return _repository.UpdateAccount(item);
        }
        public bool AddInfo(AccountInfoEntity item)
        {
            return _infoRepository.AddAccountInfo(item);
        }
        public bool UpdateInfo(AccountInfoEntity item)
        {
            return _infoRepository.UpdateAccountFullInfo(item);
        }
        public bool AddOpenId(AccountOpenIdEntity entity)
        {
            return _openIdRepository.AddOpenId(entity);
        }
        public AccountDomain GetAccountById(string accountId)
        {
            var account = _repository.GetAccountById(accountId);
            
            return ConvertOrderEntityToDomain(account);
        }

        public AccountDomain GetAccount(string account)
        {
            var entity = _repository.GetAccount(account);

            return ConvertOrderEntityToDomain(entity);
        }

        public bool ValidateLogin(string userName, string passWord)
        {
            var account = _repository.GetAccount(userName);

            return (account.Password == passWord);
        }

        public AccessTokenDomain GetAccessToken(string userKey)
        {
            var result = _tokenRepository.GetAccessTokenByKey(userKey);
            return ConvertTokenEntityToDomain(result);
        }
        public List<EventFullInfoDomain> GetDefaultEventList(int eventType)
        {
            var retList = new List<EventFullInfoDomain>();
            var list = _eventRepository.GetDefaultEventList(eventType);
            foreach(var item in list)
            {
                var entity = ConvertEvengtEntityToDomain(item);
                retList.Add(entity);
            }
            return retList;
        }

        public AccessTokenDomain GetAccessTokenByRefreshToken(string userKey)
        {
            var result = _tokenRepository.GetAccessTokenByRefreshToken(userKey);
            return ConvertTokenEntityToDomain(result);
        }

        public AccountDomain GetOtherAccount(string otherAccount, OtherType otherType)
        {
            AccountEntity account = null;
            if (otherType == OtherType.AliAccount)
            {
                account = _repository.GetAccountByAli(otherAccount);
            }
            else if(otherType == OtherType.WxAccount)
            {
                account = _repository.GetAccountByWx(otherAccount);
            }
            else if (otherType == OtherType.QQAccount)
            {
                account = _repository.GetAccountByQQ(otherAccount);
            }               

            return ConvertOrderEntityToDomain(account);
        }

        public AccountDomain GetAccountByUnionId(string wxUnionId)
        {
            var account = _repository.GetAccountByUnionId(wxUnionId);
            return ConvertOrderEntityToDomain(account);
        }

        public AccountDomain GetAccountByMobilePhone(string mobilePhone)
        {
            var account = _repository.GetAccountByMobilePhone(mobilePhone);
            return ConvertOrderEntityToDomain(account);
        }


        private AccountDomain ConvertOrderEntityToDomain(AccountEntity entity)
        {
            if(entity == null)
            {
                return null;
            }
            var account = new AccountDomain();
            account.SetMemento(entity);
            return account;
        }

        private AccessTokenDomain ConvertTokenEntityToDomain(OAuth2AccessToken entity)
        {
            if(entity == null)
            {
                return null;
            }
            var account = new AccessTokenDomain();
            account.SetMemento(entity);
            return account;
        }
        private EventFullInfoDomain ConvertEvengtEntityToDomain(EventFullInfoEntity entity)
        {
            var account = new EventFullInfoDomain();
            account.SetMemento(entity);
            return account;
        }
    }
}
