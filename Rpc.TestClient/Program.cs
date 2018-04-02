using Grpc.Core;
using MD.SmsService;
using SP.Service;
using System;
using static MD.SmsService.Sms;
using static SP.Service.AccountService;
using static SP.Service.OrderService;
using static SP.Service.ProductService;

namespace Rpc.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = new Channel(args[0] + ":" + args[1], ChannelCredentials.Insecure);
            //var client = new AccountServiceClient(channel);
            //testAddShoppingCart(client);
            //testRegistAccount(client);
            //testGetAccountDetail(client);
            //testGetAccessToken(client);
            //testAddAccessToken(client);
            //testRemoveAccessToken(client);
            //testAddAccountAddress(client);
            //testGetAddressList(client);
            //testEditAccountAddress(client);
            //testUpdateAddressStatus(client);
            //testDelAddress(client);
            //testGetRegionDataList(client);
            //testGetRegionData(client);
            //testGetAccount(client);
            //testAddAccountAuthentication(client);
            //testGetVerifyCodeRequest(client);
            //testUpdateAuthStatusByAccount(client);

            //channel = new Channel(args[0] + ":" + args[1], ChannelCredentials.Insecure);
            //var client1 = new ProductServiceClient(channel);
            //testGetTitleAttributeList(client1);
            //testGetAllShopList(client1);
            //testGetShopProductList(client1);

            var client1 = new SmsClient(channel);
            testSendMessage(client1, args[2]);
            //testSetRegisterMobileMessageLimit(client1, args[2]);
            //testCheckIsAllowSendRegisterMobileMessage(client1);

            //var client1 = new OrderServiceClient(channel);
            //testGetMyOrderList(client1);

            //testGetDiscountByAccountId(client);
        }

        private static void testRegistAccount(AccountServiceClient client)
        {
            var request1 = new RegistRequest()
            {
                Email = "",
                MobilePhone="18624423286",
                PassWord="Sa123456"
            };
            var list = client.RegistAccount(request1);
            Console.WriteLine("RegistAccount:" + list.ToString());

        }

        private static void testGetAccountDetail(AccountServiceClient client)
        {
            var request1 = new AccountIdRequest()
            {
                AccountId= "059868ad-6c4e-44ff-a7ba-32d119b23bd3"
            };
            var list = client.GetAccountDetail(request1);
            Console.WriteLine("GetAccountDetail:" + list.ToString());

        }
        private static void testGetAccount(AccountServiceClient client)
        {
            var request1 = new AccountRequest()
            {
                 Account= "+18624423286"
            };
            var list = client.GetAccount(request1);
            Console.WriteLine("GetAccount:" + list.ToString());

        }

        private static void testGetAccessToken(AccountServiceClient client)
        {
            var request1 = new TokenKeyRequest()
            {
               Key= "059868ad-6c4e-44ff-a7ba-32d119b23bd3"
            };
            var list = client.GetAccessToken(request1);
            Console.WriteLine("GetAccessToken:" + list.ToString());

        }

        private static void testAddAccessToken(AccountServiceClient client)
        {
            var request1 = new AccessTokenRequest()
            {
                AccessToken = new AccessToken()
                {
                     AccountId = "059868ad-6c4e-44ff-a7ba-32d119b23bd3",
                     AccessToken_ = "059868ad6c4e44ffa7ba32d119b23bd3",
                     AccessTokenExpires = DateTime.Now.AddDays(7).Ticks,
                     CreateTime = DateTime.Now.Ticks,
                     RefreshToken = "059868ad6c4e33ffa7ba32d119b23bd5",
                     RefreshTokenExpires= DateTime.Now.AddDays(7).Ticks,
                }
            };
            var list = client.AddAccessToken(request1);
            Console.WriteLine("AddAccessToken:" + list.ToString());

        }

        private static void testRemoveAccessToken(AccountServiceClient client)
        {
            var request1 = new TokenIdRequest()
            {
                AccessToken= "059868ad-6c4e-44ff-a7ba-32d119b23bd3",
                AccountId = "059868ad6c4e44ffa7ba32d119b23bd3",
            };
            var list = client.RemoveAccessToken(request1);
            Console.WriteLine("RemoveAccessToken:" + list.ToString());

        }

        private static void testAddAccountAddress(AccountServiceClient client)
        {
            var request1 = new AddressRequest()
            {
                Address = new Address()
                {
                  AccountId = "3ad1319a-42ea-4e68-b0d0-de72d6db45c8",
                  ContactAddress="wrwrwer",
                  ContactMobile="18624423286",
                  ContactName = "gaoxq",
                  Gender = 0,
                  DistrictId= 1
                }
            };
            var list = client.AddAccountAddress(request1);
            Console.WriteLine("AddAccountAddress:" + list.ToString());
        }

        private static void testGetAddressList(AccountServiceClient client)
        {
            var request1 = new AccountIdRequest()
            {
                AccountId = "3ad1319a-42ea-4e68-b0d0-de72d6db45c8"
            };
            var list = client.GetAddressList(request1);
            Console.WriteLine("GetAddressList:" + list.ToString());

        }
        private static void testEditAccountAddress(AccountServiceClient client)
        {
            var request1 = new AddressRequest()
            {
                Address = new Address()
                {
                    Id=1,
                    AccountId = "3ad1319a-42ea-4e68-b0d0-de72d6db45c8",
                    ContactAddress = "wrwrwer",
                    ContactMobile = "15840698745",
                    ContactName = "1212",
                }
            };
            var list = client.EditAccountAddress(request1);
            Console.WriteLine("EditAccountAddress:" + list.ToString());
        }

        private static void testUpdateAddressStatus(AccountServiceClient client)
        {
            var request1 = new AddressStatusRequest()
            {
                Id = 1,
                AccountId = "3ad1319a-42ea-4e68-b0d0-de72d6db45c8",
                Status=1
            };
            var list = client.UpdateAddressStatus(request1);
            Console.WriteLine("UpdateAddressStatus:" + list.ToString());
        }

        private static void testDelAddress(AccountServiceClient client)
        {
            var request1 = new DelAddressRequest()
            {
                Id = 3
            };
            var list = client.DelAddress(request1);
            Console.WriteLine("DelAddress:" + list.ToString());
        }

        private static void testGetRegionDataList(AccountServiceClient client)
        {
            var request1 = new RegionDataRequest()
            {
                DataType = 1
            };
            var list = client.GetRegionDataList(request1);
            Console.WriteLine("GetRegionDataList:" + list.ToString());

        }
        private static void testGetRegionData(AccountServiceClient client)
        {
            var request1 = new RegionIDRequest()
            {
                DataId= 10
            };
            var list = client.GetRegionData(request1);
            Console.WriteLine("GetRegionData:" + list.ToString());

        }

        private static void testGetTitleAttributeList(ProductServiceClient client)
        {
            var request1 = new TitleAttributeListRequest()
            {
                AttType=1
            };
            var list = client.GetTitleAttributeList(request1);
            Console.WriteLine("GetTitleAttributeList:" + list.ToString());

        }

        private static void testGetAllShopList(ProductServiceClient client)
        {
            var request1 = new ShopListRequest()
            {
                PageIndex = 1,
                PageSize = 3
            };
            var list = client.GetAllShopList(request1);
            Console.WriteLine("GetAllShopList:" + list.ToString());

        }

        private static void testGetShopProductList(ProductServiceClient client)
        {
            var request1 = new ShopProductListRequest()
            {
                ShopId=3,
                AttributeId = 2,
                PageIndex = 1,
                PageSize = 3
            };
            var list = client.GetShopProductList(request1);
            Console.WriteLine("GetShopProductList:" + list.ToString());

        }

        private static void testAddShoppingCart(AccountServiceClient client)
        {
            var request1 = new ShoppingCartRequest()
            {
                 AccountId = "05466232-d202-4f76-ba2f-6b8ddb9e0b8c",
                 ProductId = "b9945c98-39db-45b1-933d-1b0dd060ce2e",
                 Quantity = 1,
                 ShopId = 1
            };
            var list = client.AddShoppingCart(request1);
            Console.WriteLine("RegistAccount:" + list.ToString());

        }
        private static void testSendMessage(SmsClient client,string mobile)
        {
            var request1 = new SendMessageRequest()
            {
                Mobile= mobile,
                MessageType = 1,
                Message= "尊敬的用户，您的注册验证码为：1234，请勿泄漏于他人！",
                TemplateType = 1,
                RequestId=Guid.NewGuid().ToString()
            };
            var list = client.SendMessage(request1);
            Console.WriteLine("SendMessage:" + list.ToString());
        }
        private static void testSetRegisterMobileMessageLimit(SmsClient client,string mobile)
        {
            var request1 = new MD.SmsService.RegisterRequest()
            {
                MobilePhone = mobile,
                Ip="127.0.0.1"
            };
            var list = client.SetRegisterMobileMessageLimit(request1);
            Console.WriteLine("SetRegisterMobileMessageLimit:" + list.ToString());

        }
        private static void testCheckIsAllowSendRegisterMobileMessage(SmsClient client)
        {
            var request1 = new MD.SmsService.RegisterRequest()
            {
                MobilePhone = "18624423286",
                Ip = "127.0.0.1"
            };
            var list = client.CheckIsAllowSendRegisterMobileMessage(request1);
            Console.WriteLine("CheckIsAllowSendRegisterMobileMessage:" + list.ToString());

        }

        private static void testAddAccountAuthentication(AccountServiceClient client)
        {
            var request1 = new AddAuthenticationRequest()
            {
                 AccountAuth = new AccountAuthentication()
                 {
                     Account = "18623323435",
                     AccountId = "05466232-d202-4f76-ba2f-6b8ddb9e0b8c",
                     AuthType = 6,
                     VerifyCode = "1241",
                 }
            };
            var list = client.AddAccountAuthentication(request1);
            Console.WriteLine("AddAccountAuthentication:" + list.ToString());
        }

        private static void testUpdateAuthStatusByAccount(AccountServiceClient client)
        {
            var request1 = new UpdateAuthenticationRequest()
            {
                Account = "18623323435",
                AccountId = "05466232-d202-4f76-ba2f-6b8ddb9e0b8c",
                AuthType = 6,
                Status = 2
            };
            var list = client.UpdateAuthStatusByAccount(request1);
            Console.WriteLine("UpdateAuthStatusByAccount:" + list.ToString());
        }

        private static void testGetVerifyCodeRequest(AccountServiceClient client)
        {
            var request1 = new GetVerifyCodeRequest()
            {
                Account = "18623323435",
                AccountId = "05466232-d202-4f76-ba2f-6b8ddb9e0b8c",
                AuthType = 6,                
            };
            var list = client.GetValidVerifyCodeByAccount(request1);
            Console.WriteLine("GetVerifyCodeRequest:" + list.ToString());
        }

        private static void testGetMyOrderList(OrderServiceClient client)
        {
            var request1 = new MyOrderRequest()
            {
                AccountId = "05466232-d202-4f76-ba2f-6b8ddb9e0b8c",
                OrderStatus = 0,
            };
            var list = client.GetMyOrderList(request1);
            Console.WriteLine("GetMyOrderList:" + list.ToString());
        }
        private static void testGetDiscountByAccountId(AccountServiceClient client)
        {
            var request1 = new DiscountRequest()
            {
                AccountId = "05466232-d202-4f76-ba2f-6b8ddb9e0b8c",
                Kind=200
            };
            var list = client.GetDiscountByAccountId(request1);
            Console.WriteLine("GetDiscountByAccountId:" + list.ToString());

        }

    }
}
