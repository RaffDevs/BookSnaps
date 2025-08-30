using BookSnaps.Webapp.Models.Enums;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace BookSnaps.Webapp.Extensions;

public static class ControllerExtensions
{
   public static void AddToastMessage(this Controller controller, string toastMessage, ToastType toastType, string toastLabel)
   {
      controller.TempData["ToastLabel"] = toastLabel;
      controller.TempData["ToastMessage"] = toastMessage;
      controller.TempData["ToastType"] = toastType.Humanize();
   }
   
   public static void AddToastMessage(this ViewComponent component, string toastMessage, ToastType toastType, string toastLabel)
   {
      component.TempData["ToastLabel"] = toastLabel;
      component.TempData["ToastMessage"] = toastMessage;
      component.TempData["ToastType"] = toastType.Humanize();
   }
   
}