using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Read_sheet_data_api.Controllers
{
    [Route("api/[controller]")]
    public class GoogleSheetController : ControllerBase
    {
        private readonly ILogger<GoogleSheetController> _logger;

        public GoogleSheetController(ILogger<GoogleSheetController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string sheetId, string range, string accessToken)
        {
            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = GoogleCredential.FromAccessToken(accessToken),
                ApplicationName = "Google-SheetsSample/0.1",
            });

            string spreadsheetId = sheetId;


            SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum valueRenderOption = (SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum)0;  // TODO: Update placeholder value.


            SpreadsheetsResource.ValuesResource.GetRequest.DateTimeRenderOptionEnum dateTimeRenderOption = (SpreadsheetsResource.ValuesResource.GetRequest.DateTimeRenderOptionEnum)0;  // TODO: Update placeholder value.

            SpreadsheetsResource.ValuesResource.GetRequest request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
            request.ValueRenderOption = valueRenderOption;
            request.DateTimeRenderOption = dateTimeRenderOption;

            Google.Apis.Sheets.v4.Data.ValueRange response = request.Execute();

            return Ok(response.Values);
        }


        [HttpPost]
        public async Task<IActionResult> Authorise(Authorization authorization )
        {
            UserCredential data = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                             new ClientSecrets
                             {
                                 ClientId = authorization.ClientId,
                                 ClientSecret = authorization.ClientSecrert
                             },
                             authorization.Scopes,
                             authorization.UserId,
                             CancellationToken.None);

            

            return Ok(data.Token);
        }

    }
}
