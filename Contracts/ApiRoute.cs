using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public static class ApiRoute
    {
        public const string Version = "v1";
        public const string Root = "api";
        public const string Base = Version + "/" + Root;

        public static class Identity
        {
            public const string Sub = "identity";
            public const string Login = Base + "/" + Sub + "/login";
            public const string Register = Base + "/" + Sub + "/register";

        }

        public static class MovieRoute
        {
            public const string Sub = "movies";
            public const string GetAll = Base + "/" + Sub;
            public const string GetOne = Base + "/" + Sub + "/{movieId}";
            public const string Create = Base + "/" + Sub;
        }
    }
}
