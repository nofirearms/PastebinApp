using System.Threading.Tasks;

namespace PastebinApp.Model
{
	public interface IDataService
	{
		Task<DataItem> GetData();
	}
}