namespace longbox.Model
{
    public class Comic
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string RelativeToRootPath { get; set; }
        public Folder RootFolder { get; set; }
        public int ArchiveType { get; set; }
        public int PageCount { get; set; }
    }
}
