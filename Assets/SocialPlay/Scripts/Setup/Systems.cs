using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudGoods
{
    static class Systems
    {
        public static Guid AppId { get; set; }
        public static ISocialPlayerUserGetter UserGetter = new SocialPlayUserWebServiceGetter();
    }
}
