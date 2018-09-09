# TheWebScraper

Small example of using HtmlAgilityPack and reflections:


```csharp
private void ParseHtml(HtmlNode type, string htmlName, string dbName, int[] indexes, Dictionary<string, string> immobilienProperties)
{
	Type htmlType = Type.GetType("TheWebScraper." + htmlName);
	if (htmlType is null)
	{
		htmlType = typeof(BaseParsingClass);
	}

	MethodInfo methodInfo = htmlType.GetMethod("GetParsedHtml");
	object classInstance = Activator.CreateInstance(htmlType, null);

	object[] parametersArray = new object[] { type, htmlName, dbName, indexes, immobilienProperties };
	methodInfo.Invoke(classInstance, parametersArray);
}
```
