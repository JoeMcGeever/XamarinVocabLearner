using System;
using System.Collections.Generic;
using System.Text;

namespace VocabLearner.Controller
{
    public class User
    {
        public string username { get; set; }
        public byte[] password { get; set; }

        public string profilePic { get; set; }
    }
}
