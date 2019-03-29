using Lib.Application.Services;
using LibMain.Dependency;
using LibMain.Domain.Repositories;
using SP.Application.Discount.DTO;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Discount
{
    public class DiscountAppService : ApplicationService, IDiscountAppService
    {
        private readonly IRepository<SysKindEntity, string> _discountRepository;
        public DiscountAppService(IRepository<SysKindEntity, string> discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public List<SysKindDto> GetDiscountList(int kind, int pageIndex, int pageSize)
        {
            var retList = new List<SysKindDto>();
            var repository = IocManager.Instance.Resolve<SysKindRespository>();
            var list = repository.GetDiscountList(kind, pageIndex, pageSize);
            foreach (var item in list)
            {
                var discount = ConvertFromRepositoryEntity(item);
                retList.Add(discount);
            }
            return retList;
        }
        public long GetOrderListCount(int kind)
        {
            var repository = IocManager.Instance.Resolve<SysKindRespository>();
            var count = repository.GetDiscountListCount(kind);
            return count;
        }

        public bool AddSysKind(SysKindDto sysKind, int kind)
        {
            var repository = IocManager.Instance.Resolve<SysKindRespository>();
            var ret = repository.AddSysKind(new SysKindEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Num = sysKind.Num,
                Description = sysKind.Description,
                DiscountValue = sysKind.DiscountValue,
                Kind = kind,
                Price = sysKind.Price,
                Quantity = sysKind.Quantity,
                Unit = sysKind.Unit,
            });
            return ret;
        }
        public SysKindDto GetAssociatorDetail(string kindId)
        {
            var repository = IocManager.Instance.Resolve<SysKindRespository>();
            var entity = repository.GetAssociatorDetail(kindId);
            return ConvertFromRepositoryEntity(entity);
        }
        public bool EditSysKind(SysKindDto sysKind)
        {
            var repository = IocManager.Instance.Resolve<SysKindRespository>();
            return repository.EditSysKind(new SysKindEntity()
            {
                Id = sysKind.KindId,                
                Description = sysKind.Description,
                DiscountValue = sysKind.DiscountValue ,
                Price = sysKind.Price,
                Quantity = sysKind.Quantity,
                Unit = sysKind.Unit
            });
        }
        public bool DeleteSysKind(string kindId)
        {
            var repository = IocManager.Instance.Resolve<SysKindRespository>();
            return repository.DeleteSysKind(kindId);
        }

        public List<ResEventDto> GetResEventList(int pageIndex, int pageSize)
        {
            var retList = new List<ResEventDto>();
            var repository = IocManager.Instance.Resolve<ResEventRespository>();
            var list = repository.GetResEventList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var resEvent = ConvertResEventFromRepositoryEntity(item);
                retList.Add(resEvent);
            }
            return retList;
        }
        public long GetResEventListCount()
        {
            var repository = IocManager.Instance.Resolve<ResEventRespository>();
            var count = repository.GetResEventListCount();
            return count;
        }
        public bool AddResEvent(ResEventDto resEvent)
        {
            var repository = IocManager.Instance.Resolve<ResEventRespository>();
            var ret = repository.AddResEvent(new ResEventEntity()
            {
                EventName = resEvent.EventName,
                Kind = resEvent.Kind,
            });
            return ret;
        }
        public bool DeleteResEvent(int id)
        {
            var repository = IocManager.Instance.Resolve<ResEventRespository>();
            return repository.DeleteResEvent(id);
        }
        public List<EventDto> GetEventList(int pageIndex, int pageSize)
        {
            var retList = new List<EventDto>();
            var repository = IocManager.Instance.Resolve<EventRespository>();
            var list = repository.GetEventList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var resEvent = ConvertEventFromRepositoryEntity(item);
                retList.Add(resEvent);
            }
            return retList;
        }
        public long GetEventListCount()
        {
            var repository = IocManager.Instance.Resolve<EventRespository>();
            var count = repository.GetEventListCount();
            return count;
        }
        public bool DeleteEvent(int id)
        {
            var repository = IocManager.Instance.Resolve<EventRespository>();
            return repository.DeleteEvent(id);
        }
        public bool AddEvent(EventDto resEvent)
        {
            var repository = IocManager.Instance.Resolve<EventRespository>();
            var ret = repository.AddEvent(new EventRelationEntity()
            {
                KindId = resEvent.KindId,
                Quantity = resEvent.Quantity,
                ResEventId = resEvent.ResEventId,
                SysEventId = resEvent.SysEventId
            });
            return ret;
        }
        public List<SysKindDto> GetSysKindList(int resEventId)
        {
            var retList = new List<SysKindDto>();
            var repository = IocManager.Instance.Resolve<SysKindRespository>();
            var list = repository.GetSysKindList(resEventId);
            foreach (var item in list)
            {
                var discount = ConvertFromRepositoryEntity(item);
                retList.Add(discount);
            }
            return retList;
        }
        public List<ResEventDto> GetResEventList()
        {
            var retList = new List<ResEventDto>();
            var repository = IocManager.Instance.Resolve<ResEventRespository>();
            var list = repository.GetResEventList();
            foreach (var item in list)
            {
                var resEvent = ConvertResEventFromRepositoryEntity(item);
                retList.Add(resEvent);
            }
            return retList;
        }
        public List<SysEventDto> GetSysEventList()
        {
            var retList = new List<SysEventDto>();
            var repository = IocManager.Instance.Resolve<ResEventRespository>();
            var list = repository.GetSysEventList();
            foreach (var item in list)
            {
                var resEvent = ConvertSysEventFromRepositoryEntity(item);
                retList.Add(resEvent);
            }
            return retList;
        }
        public List<CarouselDto> GetCarouselList(int pageIndex, int pageSize)
        {
            var retList = new List<CarouselDto>();
            var repository = IocManager.Instance.Resolve<CarouselRespository>();
            var list = repository.GetCarouselList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var resEvent = ConvertCarouselFromRepositoryEntity(item);
                retList.Add(resEvent);
            }
            return retList;
        }
        public long GetCarouselListCount()
        {
            var repository = IocManager.Instance.Resolve<CarouselRespository>();
            var count = repository.GetCarouselListCount();
            return count;
        }
        public bool DeleteCarousel(int id)
        {
            var repository = IocManager.Instance.Resolve<CarouselRespository>();
            return repository.DeleteCarousel(id);
        }
        public bool AddCarousel(CarouselDto carousel)
        {
            var repository = IocManager.Instance.Resolve<CarouselRespository>();
            var ret = repository.AddCarousel(new CarouselEntity()
            {
                Description = carousel.Description,
                DisplaySequence = carousel.DisplaySequence,
                ImagePath = carousel.ImagePath,
                Url = carousel.Url
            });
            return ret;
        }
        public List<EventDto> GetDefaultEventList(int eventType)
        {
            var retList = new List<EventDto>();
            var repository = IocManager.Instance.Resolve<EventRespository>();
            var list = repository.GetDefaultEventList(eventType);
            foreach (var item in list)
            {
                var resEvent = ConvertEventFromRepositoryEntity(item);
                retList.Add(resEvent);
            }
            return retList;
        }
        private static SysKindDto ConvertFromRepositoryEntity(SysKindEntity sysKind)
        {
            if (sysKind == null)
            {
                return null;
            }
            var sysKindDto = new SysKindDto
            {
                Description = sysKind.Description,
                DiscountValue = sysKind.DiscountValue == null ? 0 : sysKind.DiscountValue.Value,
                Num = sysKind.Num != null ? sysKind.Num.Value:0,
                KindId = sysKind.Id,
                Price = sysKind.Price == null ? 0 : sysKind.Price.Value,
                Quantity = sysKind.Quantity.Value,
                Unit = sysKind.Unit.Value,                
            };
            return sysKindDto;
        }
        private static ResEventDto ConvertResEventFromRepositoryEntity(ResEventEntity resEvent)
        {
            if (resEvent == null)
            {
                return null;
            }
            var resEventDto = new ResEventDto
            {
                Id = resEvent.Id,
                EventName = resEvent.EventName,
                Kind = resEvent.Kind.Value,
            };
            return resEventDto;
        }
        private static EventDto ConvertEventFromRepositoryEntity(EventFullInfoEntity eventFullInfo)
        {
            if (eventFullInfo == null)
            {
                return null;
            }
            var eventDto = new EventDto
            {
                Id = eventFullInfo.Id,
                KindId = eventFullInfo.KindId,
                Quantity = eventFullInfo.Quantity,
                ResEventId = eventFullInfo.ResEventId,
                SysEventId = eventFullInfo.SysEventId,
                Description = eventFullInfo.Description,
                ResEventName = eventFullInfo.ResEventName,
                SysEventName = eventFullInfo.SysEventName
            };
            return eventDto;
        }
        private static SysEventDto ConvertSysEventFromRepositoryEntity(SysEventEntity resEvent)
        {
            if (resEvent == null)
            {
                return null;
            }
            var resEventDto = new SysEventDto
            {
                Id = resEvent.Id,
                EventName = resEvent.EventName,
                EventType = resEvent.EventType.Value,
            };
            return resEventDto;
        }
        private static CarouselDto ConvertCarouselFromRepositoryEntity(CarouselEntity carousel)
        {
            if (carousel == null)
            {
                return null;
            }
            var carouselDto = new CarouselDto
            {
                Id = carousel.Id,
                Description = carousel.Description,
                Url = carousel.Url,
                ImagePath = carousel.ImagePath,
                DisplaySequence = carousel.DisplaySequence
            };
            return carouselDto;
        }
    }
}
