namespace DevFreela.API.Services
{
    
    public interface IConfigService
    {
        int getValue();
    }
    public class ConfigService : IConfigService
    {
        private int _value;
        public int getValue()
        {
            _value++;

            return _value;
        }
    }
}
