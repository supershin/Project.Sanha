using System;
namespace Project.Sanha.Web.Common
{
	public static class SystemConstant
	{
		public class Status
		{
            public const int ALL = 0;
            public const int WAIT = 1;
            public const int SUCCESS = 2;
			public const int REJECT = 3;

            public class Desc
            {
                public const String ALL = "ทั้งหมด";
                public const String WAIT = "รออนุมัติ";
                public const String SUCCESS = "อนุมัติ";
                public const String REJECT = "ไม่อนุมัติ";
            }
            public static String Get_Desc(int status)
            {
                switch (status)
                {
                    case SystemConstant.Status.ALL:
                        return SystemConstant.Status.Desc.ALL;
                    case SystemConstant.Status.WAIT:
                        return SystemConstant.Status.Desc.WAIT;
                    case SystemConstant.Status.SUCCESS:
                        return SystemConstant.Status.Desc.SUCCESS;
                    case SystemConstant.Status.REJECT:
                        return SystemConstant.Status.Desc.REJECT;
                }
                return String.Empty;
            }
        }

        public class ResourceType
        {
            public const int IMAGE = 1;
            public const int SIGNCUST = 2;
            public const int SIGNSTAFF = 3;
            public const int PDF = 4;
        }
	}
}

