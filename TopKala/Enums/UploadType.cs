namespace TopKala.Enums
{
    public class UploadType
    {
        public string Value { get; private set; }
        private UploadType(string value) { Value = value; }
        public static UploadType Unspecified { get { return new UploadType(""); } }
        public static UploadType Profile { get { return new UploadType("Profile"); } }
        public static UploadType File { get { return new UploadType("File"); } }
    }
}