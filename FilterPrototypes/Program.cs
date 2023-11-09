using System.Text;

namespace FilterPrototypes
{
    internal enum SearchKey
    {
        Username = 0,
        Email = 1,
        Title = 2,
        Ingrediant = 3
    }
    
    internal class ParsingOptions
    {
        public bool WithUsername { get; set; } = false;

        public bool WithEmail { get; set; } = false;

        public bool WithTitle { get; set; } = false;

        public bool WithIngrediant { get; set; } = false;
    }

    internal class GetParameter
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string SearchQuery { get; set; }

        public string BuildFilterQueryPart(ParsingOptions options)
        {
            Dictionary<SearchKey, string> keyValuePairs = new Dictionary<SearchKey, string>();

            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                return string.Empty;
            }

            foreach (string keyValue in SearchQuery.Split(','))
            {
                string normalized = keyValue.Trim();    
                // keyValue must follow the rule key:value
                if (normalized.Count(x => x == ':') != 1)
                {
                    throw new Exception("Bad request because wrong search term format");
                }

                string[] elements = normalized.Split(":");

                switch (elements[0])
                {
                    case "username":
                        if (!string.IsNullOrWhiteSpace(elements[1]) && options.WithUsername)
                            keyValuePairs.Add(SearchKey.Username, elements[1].Trim());
                        break;
                    case "email":
                        if (!string.IsNullOrWhiteSpace(elements[1]) && options.WithEmail)
                            keyValuePairs.Add(SearchKey.Email, elements[1].Trim());
                        break;
                    case "title":
                        if (!string.IsNullOrWhiteSpace(elements[1]) && options.WithTitle)
                            keyValuePairs.Add(SearchKey.Title, elements[1].Trim());
                        break;
                    case "ingrediant":
                        if (!string.IsNullOrWhiteSpace(elements[1]) && options.WithIngrediant)
                            keyValuePairs.Add(SearchKey.Ingrediant, elements[1].Trim());
                        break;
                    default:
                        throw new Exception("Bad request because search term is not supported");
                        break;
                }
            }

            if (keyValuePairs.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder queryPart = new StringBuilder();

            queryPart.Append("WHERE ");

            bool first = true;

            foreach (KeyValuePair<SearchKey, string> pair in keyValuePairs)
            {
                if (!first)
                {
                    queryPart.Append(" OR ");
                }

                first = false;
                switch (pair.Key)
                {
                    case SearchKey.Username:
                        queryPart.Append($"username='{pair.Value}'");
                        break;
                    case SearchKey.Email:
                        queryPart.Append($"email='{pair.Value}'");
                        break;
                    case SearchKey.Title:
                        queryPart.Append($"title='{pair.Value}'");
                        break;
                    case SearchKey.Ingrediant:
                        queryPart.Append($"name='{pair.Value}'");
                        break;
                }               
            }

            return queryPart.ToString();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            GetParameter parameter = new GetParameter()
            {
                PageIndex = 1,
                PageSize = 100,
                SearchQuery = "username=hello"
            };

            string queryPart = parameter.BuildFilterQueryPart(new ParsingOptions()
            {
                WithEmail = true,
                WithUsername = true,
                WithTitle = true
            });

            Console.WriteLine(queryPart);
        }
    }
}