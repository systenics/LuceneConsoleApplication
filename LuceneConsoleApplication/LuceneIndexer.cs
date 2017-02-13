using System;
using System.Collections.Generic;
using System.Configuration;

namespace LuceneConsoleApplication
{
    public class LuceneIndexer : IDisposable
    {

        Lucene.Net.Analysis.Standard.StandardAnalyzer analyzer = null;
        Lucene.Net.Index.IndexWriter writer = null;

        public void CreateLuceneIndexes()
        {
            try
            {
                StartLuceneIndexCreateProcess();
            }
            catch
            {
                throw;
            }
        }

        private void StartLuceneIndexCreateProcess()
        {
            string luceneIndexStoragePath = @ConfigurationManager.AppSettings["LuceneIndexStoragePath"];
            bool folderExists = System.IO.Directory.Exists(luceneIndexStoragePath);
            if (!folderExists)
                System.IO.Directory.CreateDirectory(luceneIndexStoragePath);

            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(luceneIndexStoragePath));
            writer = new Lucene.Net.Index.IndexWriter(directory, analyzer, true, Lucene.Net.Index.IndexWriter.MaxFieldLength.LIMITED);
            try
            {
                // We will populate below list to create Lucene index.
                List<string> actorsList = new List<string>();
                actorsList.Add("Johnny Depp");
                actorsList.Add("Robert Downey Jr.");
                actorsList.Add("Johnny Depp");
                actorsList.Add("Tom Cruise");
                actorsList.Add("Brad Pitt");
                actorsList.Add("Tom Hanks");
                actorsList.Add("Denzel Washington");
                actorsList.Add("Russell Crowe");
                actorsList.Add("Kate Winslet");
                actorsList.Add("Christian Bale");
                actorsList.Add("Hugh Jackman");
                actorsList.Add("Will Smith");
                actorsList.Add("Sean Connery");

                foreach (var item in actorsList)
                {
                    Console.WriteLine(item);
                    writer.AddDocument(CreateDocument(item.ToString()));
                }
            }
            catch
            {
                Lucene.Net.Index.IndexWriter.Unlock(directory);
                throw;
            }
            finally
            {
                writer.Optimize();
                analyzer.Close();
                writer.Dispose();
                analyzer.Dispose();
            }

        }

        private Lucene.Net.Documents.Document CreateDocument(string actorName)
        {
            try
            {
                Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();
                doc.Add(new Lucene.Net.Documents.Field("actors", actorName, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED));
                return doc;
            }
            catch
            {
                throw;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (writer != null)
                    writer.Dispose();
                if (analyzer != null)
                    analyzer.Dispose();
            }
            // free native resources if there are any.

        }

    }
}
