using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evonik.FPASSMail
{
    class Program
    {

        static void Main(string[] args)
        {
            // Startup similar to main FPASS. Read config info, initialise etc.
            FPASSStart StartUp = new FPASSStart();
            StartUp.Start();

            // The MailSender class is the interesting bit.
            MailSender sender = new MailSender();
            sender.RemindAll();
        }
    }
}
