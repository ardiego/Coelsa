using NLog;

namespace Coelsa.Common
{
    public class NLogger
    {
        private Logger _Logger = null;
        public Logger Write
        {
            get
            {
                if (_Logger == null)
                {
                    _Logger =  LogManager.GetLogger("Logger");
                }
                return _Logger;
            }
        }
    }
}
