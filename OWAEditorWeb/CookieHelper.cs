using System;
using System.Web;

namespace OWAEditorWeb
{
    /// <summary>
    /// ��Cookie�����ķ�װ
    /// </summary>
    public class CookieHelper
    {
        private HttpContext context;		//��ȡHTTP�������Ϣ
        private HttpCookie acookie;			//cookie��
        private int operateState = 0;

        public CookieHelper()
        {
            context = HttpContext.Current;
        }

        /// <summary>
        /// �жϿͻ����Ƿ�֧��ʹ��Cookie
        /// </summary>
        /// <returns>���ص��ǲ����ͱ���,�����жϿͻ����Ƿ�֧��Cookie</returns>
        public bool Estop()
        {
            //�жϿͻ����Ƿ�֧��Cookies
            if (context.Request.Browser.Cookies)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ����һ���й���ʱ���cookie
        /// </summary>
        /// <param name="cookieName">cookie��</param>
        /// <param name="cookieValue">cookieֵ</param>
        /// <param name="time">cookie�Ĺ���ʱ��</param>
        /// <returns>����operateState��������ɹ�
        ///	����operateState + 3����ͻ��˲�֧��Cookie</returns>
        public int CreateCookie(string cookieName, string cookieValue, DateTime time)
        {
            //�жϿͻ����Ƿ�֧��Cookie
            if (Estop() == false)
            {
                return operateState + 3;
            }
            acookie = new HttpCookie(cookieName, cookieValue);
            acookie.Expires = time;

            context.Response.SetCookie(acookie);

            //context.Response.SetCookie(acookie);
            return 0;
        }

        /// <summary>
        /// ���һ�������ڵ�cookie
        /// </summary>
        /// <param name="cookieName">cookie��</param>
        /// <param name="cookieValue">cookieֵ</param>
        /// <returns>����operateState��������ɹ�
        ///	����operateState + 3����ͻ��˲�֧��Cookie</returns>
        public int CreateCookie(string cookieName, string cookieValue)
        {
            //�жϿͻ����Ƿ�֧��Cookie
            if (Estop() == false)
            {
                return operateState + 3;
            }

            acookie = new HttpCookie(cookieName, cookieValue);

            context.Response.SetCookie(acookie);

            return 0;
        }

        /// <summary>
        ///  �ж�ָ����cookie�Ƿ����
        /// </summary>
        /// <param name="cookieName">cookie��</param>
        /// <param name="being">������ж��Ƿ���ڵĲ���ֵ</param>
        /// <returns>����operateState��������ɹ�
        ///	����operateState + 3����ͻ��˲�֧��Cookie</returns>
        public int Exists(string cookieName, out bool being)
        {
            being = false;
            //�жϿͻ����Ƿ�֧��Cookie
            if (Estop() == false)
            {
                return operateState + 3;
            }

            acookie = context.Request.Cookies.Get(cookieName);
            if (acookie != null && acookie.Value != "")
                being = true;
            else
                being = false;
            return 0;
        }

        /// <summary>
        /// ��ȡָ����cookie��ֵ
        /// </summary>
        /// <param name="cookieName">cookie��</param>
        /// <param name="cookieValue">����͵�cookie��ֵ</param>
        /// <returns>����operateState��������ɹ�,����operateState + 2����ָ����cookie������
        ///	����operateState + 3����ͻ��˲�֧��Cookie</returns>
        public int GetCookieValue(string cookieName, out string cookieValue)
        {
            cookieValue = null;
            //�жϿͻ����Ƿ�֧��Cookie
            if (Estop() == false)
            {
                return operateState + 3;
            }

            bool being;
            //���ñ�����ж��Ƿ���ֵ�ĺ������ж�ָ��cookie�Ƿ���ֵ
            Exists(cookieName, out being);

            //���cookie���ھͻ�ȡcookie��ֵ,���򷵻ش������2
            if (being == true)
            {
                cookieValue = context.Request.Cookies.Get(cookieName).Value;
            }
            else
            {
                cookieValue = null;
                return operateState + 2;
            }
            return 0;
        }

        /// <summary>
        /// ɾ��һ��ָ����Cookie
        /// </summary>
        /// <param name="cookieName">cookie��</param>
        /// <returns>����operateState��������ɹ�
        ///	����operateState + 3����ͻ��˲�֧��Cookie</returns>
        public int DeleteCookie(string cookieName)
        {
            //�жϿͻ����Ƿ�֧��Cookie
            if (Estop() == false)
            {
                return operateState + 3;
            }
            CreateCookie(cookieName, "");
            return 0;
        }

        /// <summary>
        /// ������е�cookie��
        /// </summary>
        /// <param name="cookieName">����Ͳ����û��������cookie,���ʱ���Ը�cookie�����й��ɵ�,ȡ��ʱ��Ϳ����жϷֿ���ͬ��Ŀ֮���cookie</param>
        /// <returns></returns>
        public int GetAllCookieName(out string[] cookieName)
        {
            HttpCookieCollection allCookie = context.Request.Cookies;
            System.Collections.IEnumerator e = allCookie.GetEnumerator();
            cookieName = new string[allCookie.Count];
            for (int i = 0; i < allCookie.Count; i++)
            {
                e.MoveNext();
                if (e.Current.ToString() != "ASP.NET_SessionId")
                {
                    cookieName[i] = e.Current.ToString();
                }
            }
            return operateState;
        }
    }
}