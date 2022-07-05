using STM.AIU.Application.Contracts.Helpers;

namespace STM.AIU.Application.Helpers;
internal class GeneralHelpers : IGeneralHelpers
{
    public string StringFixer(string inputValue)
    {
        if (inputValue.Trim() != "")
        {
            inputValue = inputValue.Replace("'", "''");
        }
        return inputValue;
    }
    public string GetUserStatus(int strStatus)
    {
        if (strStatus == 1)
        {
            return "Active";
        }
        else
        {
            return "Inactive";
        }
    }
}
