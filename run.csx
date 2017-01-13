#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ICollector<Request> outTable, TraceWriter log)
{
    dynamic data = await req.Content.ReadAsAsync<object>();
    string name = data?.userName;

    if (name == null)
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Please provide an user name in the request body");
    }

    outTable.Add(new Request()
    {        
        PartitionKey = "Functions",
        RowKey = Guid.NewGuid().ToString(),
        cityName = data.cityName,
        convertedLocalTime = data.convertedLocalTime,
        convertedRequestedTime = data.convertedRequestedTime,
        dstOffset = data.dstOffset,
        rawOffset = data.rawOffset,
        timeAtLocation = data.timeAtLocation,
        timeZoneId  = data.timeZoneId,
        timeZoneName = data.timeZoneName,
        userName = data.userName
    });
    return req.CreateResponse(HttpStatusCode.Created);
}

public class Request : TableEntity
{
    public string cityName { get; set; }
    public string convertedLocalTime { get; set; }
    public string convertedRequestedTime { get; set; }
    public int dstOffset { get; set; }
    public int rawOffset { get; set; }
    public int timeAtLocation { get; set; }
    public string timeZoneId { get; set; }
    public string timeZoneName { get; set; }
    public string userName { get; set; }
}