using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProvaCandidato.Helper
{
    public static class MessageHelper
    {
        public static void DisplaySuccessMessage(Controller controller, string message)
        {
            var userMessage = new { CssClassName = "", Title = "Sucesso", Message = message };
            controller.TempData["UserMessage"] = message;
        }

        public static void DisplayErrorMessage(Controller controller, string message)
        {
            var userMessage = new { CssClassName = "", Title = "Erro", Message = message };
            controller.TempData["UserMessage"] = message;
        }
    }
}