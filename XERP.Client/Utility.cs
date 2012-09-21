

namespace XERP.Client
{

    public enum MessageTokens
    {
        None,
        StartUpLogInToken,
        CompanySearchToken,
        CompanyTypeSearchToken,
        CompanyCodeSearchToken,
        SystemUserSearchToken,
        SystemUserTypeSearchToken,
        SystemUserCodeSearchToken,
        SecurityGroupSearchToken,
        SecurityGroupTypeSearchToken,
        SecurityGroupCodeSearchToken,
        AddressSearchToken,
    };

    public class Utility
    {
        //public static byte[] ImageToBinary(string imagePath)
        //{
        //    FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
        //    byte[] buffer = new byte[fileStream.Length];
        //    fileStream.Read(buffer, 0, (int)fileStream.Length);
        //    fileStream.Close();
        //    return buffer;
        //}

        //public static Image BinaryToImage(System.Data.Linq.Binary binaryData)
        //{
        //    if (binaryData == null) return null;

        //    byte[] buffer = binaryData.ToArray();
        //    MemoryStream memStream = new MemoryStream();
        //    memStream.Write(buffer, 0, buffer.Length);
        //    return Image.FromStream(memStream);
        //}

        public Utility()
        {
            
        }
        
    }

}
