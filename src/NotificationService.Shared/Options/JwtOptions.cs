using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Shared.Options
{
    public class JwtOptions
    {

        public string Issuer { get; set; }
        public string Audiance { get; set; }
        public string SecretKey { get; set; }
        public int ExpirationInHours { get; set; }

    }
}
