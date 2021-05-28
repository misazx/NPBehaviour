using ETModel;

namespace ETHotfix
{
	[Config((int)(AppType.ClientH |  AppType.ClientM))]
	public partial class Client_TaskCanvasConfigCategory : ACategory<Client_TaskCanvasConfig>
	{
	}

	public class Client_TaskCanvasConfig: IConfig
	{
		public long Id { get; set; }
		public long NPBehaveId;
	}
}
