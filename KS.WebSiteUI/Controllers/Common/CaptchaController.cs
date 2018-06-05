using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using KS.Core.UI;
using KS.WebSiteUI.Controllers.Base;
using KS.Core.SessionProvider.Base;

namespace KS.WebSiteUI.Controllers.Common
{
    public class CaptchaController : BasePublicWebApiController
    {
        // GET api/<controller>
        private readonly ISessionManager _session;

        public CaptchaController(ISessionManager session)
        {
            //_session = new CustomSessionProvider(Session);
            _session = session;
        }

        //public JsonResult Genrate()
        //{
        //    System.Drawing.FontFamily family = new System.Drawing.FontFamily("Arial");
        //    CaptchaImage img = new CaptchaImage(150, 50, family);
        //    string text = img.CreateRandomText(4) + " " + img.CreateRandomText(3);
        //    img.SetText(text);
        //    img.GenerateImage();
        //    img.Image.Save(Server.MapPath("~") +
        //    this.Session.SessionID.ToString() + ".png",
        //    System.Drawing.Imaging.ImageFormat.Png);
        //    Session["captchaText"] = text;
        //    return Json(this.Session.SessionID.ToString() + ".png?t=" +
        //    DateTime.Now.Ticks, JsonRequestBehavior.AllowGet);

        //    //return new string[] { "value1", "value2" };
        //}

        [Route("Captcha/Genrate")]
        [HttpGet]
        public string Genrate()
        {
            //System.Drawing.FontFamily family = new System.Drawing.FontFamily("Arial");
            //CaptchaImage img = new CaptchaImage(150, 50, family);
            //string text = img.CreateRandomText(4) + " " + img.CreateRandomText(3);
            //_session["Captcha"] = text;
            //img.SetText(text);
            //img.GenerateImage();
            //MemoryStream ms = new MemoryStream();
            //img.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            //result.Content = new ByteArrayContent(ms.ToArray());
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            //return result;

             var family = new System.Drawing.FontFamily("Arial");
            var img = new CaptchaImage(150, 50, family);
            //string text = img.CreateRandomText(4) + " " + img.CreateRandomText(3);
            var text = img.CreateRandomText(4);
            _session.Store("Captcha",text);
            img.SetText(text);
            img.GenerateImage();
            var ms = new MemoryStream();
            img.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return Convert.ToBase64String(ms.ToArray());

            
        }
    }
}