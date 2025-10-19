using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;

FileDataStore fileDataStore = new FileDataStore(".Google.Access.Filesyncer", false);

var cred = GoogleClientSecrets.FromFile("/home/lucas/.credentials.json");

var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
    cred.Secrets,
    new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive },
    "lucas",
    CancellationToken.None,
    fileDataStore
    )
;

var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
{
    HttpClientInitializer = credential,
});

//bookid
/*
var listRequest = service.Files.List();
listRequest.Q = "'1_fUrdnkIhL384QVcoDDUEHAZ6EiKtYkk' in parents";
var listResult = await listRequest.ExecuteAsync();

foreach (var e in listResult.Files)
{
    Console.WriteLine($"{e.Name} \t {e.Id}\t{e.MimeType}");
}
*/

var fileMetadata = new Google.Apis.Drive.v3.Data.File()
{
    Name = "zeit2.pdf"
};

using (FileStream fs =
File.OpenRead("/home/lucas/Documents/Books/zeit-studienfuhrer-2025-05-10.pdf"))
{
    var request = service.Files.Create(fileMetadata, fs, "application/pdf");
    var result = await request.UploadAsync();
}


/*
var getRequest = service.Files.Get("1kJGKbKy4HuKaBdDhcOLEG0WhRvaxe5kX");
getRequest.DownloadAsync("/home/lucas/Downloads/testfile.zip").Wait();
*/