using Lib.Application.Services;
using SP.Application.Discount.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Discount
{
    public interface IDiscountAppService : IApplicationService
    {
        List<SysKindDto> GetDiscountList(int kind, int pageIndex, int pageSize);
        long GetOrderListCount(int kind);
        bool AddSysKind(SysKindDto sysKind, int kind);
        SysKindDto GetAssociatorDetail(string kindId);
        bool EditSysKind(SysKindDto sysKind);
        bool DeleteSysKind(string kindId);
        List<ResEventDto> GetResEventList(int pageIndex, int pageSize);
        long GetResEventListCount();
        bool AddResEvent(ResEventDto resEvent);
        bool DeleteResEvent(int id);
        List<EventDto> GetEventList(int pageIndex, int pageSize);
        long GetEventListCount();
        bool DeleteEvent(int id);
        bool AddEvent(EventDto resEvent);
        List<SysKindDto> GetSysKindList(int resEventId);
        List<ResEventDto> GetResEventList();
        List<SysEventDto> GetSysEventList();
        List<CarouselDto> GetCarouselList(int pageIndex, int pageSize);
        long GetCarouselListCount();
        bool DeleteCarousel(int id);
        bool AddCarousel(CarouselDto carousel);
        List<EventDto> GetDefaultEventList(int eventType);
    }
}
