namespace ControleLancamento.Api.Shared.Extension
{
    public static class ExtensionGuid
    {
        public static bool ValidationGuid(this Guid guid)
        {
            bool isValid = false;

            if (guid != Guid.Empty)
                isValid = Guid.TryParse(guid.ToString(), out var guidOutput);

            return isValid;
        }
    }
}