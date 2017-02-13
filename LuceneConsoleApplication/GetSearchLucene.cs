using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;


namespace LuceneConsoleApplication
{
    public class GetSearchLucene
    {
        public List<LuceneSearcher.LuceneData> GetSearchLuceneText(string actorName)
        {
            LuceneSearcher searchIndex = new LuceneSearcher(GetLuceneIndexPath().FullName);
            List<LuceneSearcher.LuceneData> results = new List<LuceneSearcher.LuceneData>();

            if (actorName != string.Empty)
            {
                results = searchIndex.Search(actorName, "actors").ToList<LuceneSearcher.LuceneData>();
            }
            return results;
        }

        // Get directory path for Lucene index creation
        public DirectoryInfo GetLuceneIndexPath()
        {
            return new DirectoryInfo(ConfigurationManager.AppSettings["LuceneIndexStoragePath"]);
        }
    }
}
