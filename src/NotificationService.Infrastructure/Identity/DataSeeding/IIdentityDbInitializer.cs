using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Identity.DataSeeding
{
    public interface IIdentityDbInitializer
    {


        public Task InitilaizeAssync();
    }
}
