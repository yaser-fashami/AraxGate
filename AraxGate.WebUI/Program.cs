using AraxGate.Application;
using AraxGate.Application.Dashboard;
using AraxGate.Application.Operation;
using AraxGate.Core.Domain.Entities;
using AraxGate.Core.Domain.Interfaces;
using AraxGate.Infra.Data.Sql.EFRepositories;
using AraxGate.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

string conStr = string.Empty;
if (builder.Environment.IsDevelopment())
{
    conStr = builder.Configuration.GetConnectionString("DevelopmentConnection");
}
else
{
    conStr = builder.Configuration.GetConnectionString("AraxGateDbContextConnection");
}

builder.Services.AddDbContext<AraxGateDbContext>(c => c.UseSqlServer(conStr, c=>c.UseCompatibilityLevel(110)));

builder.Services.AddIdentity<User, IdentityRole>(option=> {
    option.Password.RequiredLength = 8;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireDigit= true;
    option.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AraxGateDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly(), options =>
//{
//    options.TagName = "nav";
//    options.TagClasses = "";
//    options.OlClasses = "breadcrumb breadcrumb-separatorless fw-semibold fs-7 my-0 pt-1";
//    options.LiClasses = "breadcrumb-item text-muted";
//    options.ActiveLiClasses = "breadcrumb-item active";
//    options.SeparatorElement = "<li class=\"breadcrumb-item\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t<span class=\"bullet bg-gray-400 w-5px h-2px\"></span>\r\n\t\t\t\t\t\t\t\t\t\t\t</li>";
//    options.DefaultAction = "List";
//});

builder.Services.AddHttpClient("NoProxyClient")
    .ConfigurePrimaryHttpMessageHandler(() =>
    new HttpClientHandler
    {
        UseProxy = false
    });

#region IOC
builder.Services.AddSingleton<IAppVersionService, AppVersionService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IBasicInfoRepository, BasicInfoRepository>();
//builder.Services.AddScoped<IBasicInfoService, BasicInfoService>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IOilTankGateService, OilTankGateService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/BasicInfo/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Operation}/{action=OilTankGateOperation}/{id?}");

app.Run();


//Be Yade Ahmad
//BGGBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGBBBBBBB
//#BBB####################################################BBBBBBGBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBGGB
//#B####################################BBBBBBBBBBBBBBBBBBBBBBB#BYBBBB5B#############################BBBBBBBBBBBBBBBBBBBBBBBGGB
//#BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB#####BBGP5YJJJYYY5PGBBB#BYBBBBBPB########B#BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBGGB
//BGBBBBBBBBBB#############################BG5J?7!!~!!77!!!!!7JJJ7~~~~~JGBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBGGB
//#BB##############BBBBBBBBBBBBBBBBBBBBBGGPJ7!!!!~~~~~!!!~^^^^~~!!!!!!!75GB##BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBGGB
//BGGBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBGPPY!!!~~~~^~~~!!!~!~!!!~~~~^^^^:^^~?YGBB####BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBGGPB
//BGGGBBBBBBBBBBBBGGGGGGGGGGGBBBBBBBGPGP?!~~~~~^^^~^^^^~~~~~!~~~~~!~~~~^::::^!?Y5PGBBB#BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB##BBB#
//BGGGBBBBBBBBGGGGPPGGGGGGGGGGGGGGPP5PY!^^~~~~~!~!~:^^^^^^^::.....:^~~!!!77~^^:^^~!7J5GBBBBBBBBB###########&&&&&&&&&&@@@@@@@@@@
//BGGGGGBGGBGGGGGGG5GGGGGGGGGGGPPP5YJ!~~~!~~^:......:::~~~^^^::.....:::::^~~~~^:::..:~?5B#&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//BPGGGGGGBGGBBGGBBGGPGBGGGGPGGP55?77!!~~^^^::::::^^^^^^^^^^^^::....::^^^^:::^~~:::::..:?G&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//BPGGGGGBGPBBBBBBBBBGPPBB#BBPYJ!~~^^:.........::::...........:..:::::::::::.::^~~:.:~:..:!P&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//BBB###&&&&&&&&&@@@@@@@&#BPJ7^::....  ...................................:.....:^^^:..::...!G@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//&@@@@@@@@@&#&#&&@@@@&BP7~^:................::....................................::::..:.. :J&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@&GG&@@@@@&&&P7~^^:..................:::^^^:...::^^^^^:..:^^^^:.....  .........::::.. .?&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@#G##&&&&&#?~::....................::^^~~!7!^:^^^^^^:::::.:::::.......  .      ...:....!&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@&B##&&&&Y^:....................   ...:^^~!!!~~^^::......::...............  .......:.. ^&@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@&##&BY~::....................:^^^:.........:^^^^^::......................      ...... Y@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@&G7^::............  ......:^^~~~~~^^::..  ...:::::......     ...............   .....!@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@&?^:::.......................::::::^^:::..    ......... .        ....   ..     ....:7@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@#?~^^::................         .............      .......            .       ..  ...^&@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@J^:........:^^^^^^::::.......... ..............        ......               ......:...P@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@&!::.......:^!!7?????777!!~^::::....::.. ..:..... .         ...               . ...:::~&@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@&7^:.......^7JYPPPPGP55YYJ??7!~~~~~~~~~^:. .::::...           ....              ...:^^G@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@P~^:......~J5PGGGGBGPP55YYJJ??7??????77!!^:..:^^::...            ....           ...:^5@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@#!::....:!5PGGBBGGGPP5YYJJ????JJ?JJJ?????7!^..:^::.....       ..  ...  ......  ....:G@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@5^....::7PGBBBBGGPP55YYYYYJJJJJJJJJJJJ?J???7~^^^^^^^^^::..        ......        ..~#@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@P~:...^?PBBBBGGGGPPPPP5YYYYYYYYJJJJYYYYYYYJJ?7!!!!!!!~~~^::.....:^:....  ... ....^#@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@&Y^:.:^7PBBBGGGGBBBGGGGPPPPP555YY5555PPPP55YYJJJJ????7777777777777!~:......   ...:B@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@B!^::^7PGBBBBBBBBBBBBBBGGGGGGPP55PPPPPPPP555555YYY5555555PPP5YY5YY?!^..... .....:5@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@G~::^!5GBBBBBBBB#########BBBBBGGGGPPP5555555PPPPPP55PPPGGGGGPPPP55Y7~:.........:G@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@?^::!5BBBBBBBBBBBBBBBBBBBBBBBBGGPPP55YYYYYYJ?7!!!!!7?JJYYY55555YPP5Y~...  ..::^G##&@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@#~::7PBBBBBBBBGBGGGGGGGGGPPPPPP555YY?7!^^::......::::::^^^!77?Y5PPPPJ: .:!?^~?555?7G@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@J::?GB#BBBGGGP5555YJJYYYYJY5YYY???!~^:... ...:^~~~~~!!!!~!7JJJY55PPY!7YY7~7JJ?7!~!J@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@B^:?GBBGPJ!~~^:::::::^~!777??77!~^^^::...::^~~~~~!!!!~!~~^~!7777??JJ?!:..~J?7?JY7!J&@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@B~7BBP?!^:....    .....::^~~~~~^^^^::.........:^^::..::^^~^...:!JJJJ7:..:7~!?YPY7?&@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@!^PGJ!?J?!~~^^:::........^!7!!~^:::....:....^~~^^^^^^^~!?5?.!PP55P5Y!:..!?:~!?Y?Y@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@B^..7J?!7!~:::.....:::.......:^^:..^^^^^::^^^^~~^^^^^~~!?YPGB^7PPPGGP5?^..~G7~!7JJG@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@G~^~^^7?J?!:.:~....~!~:::^:..~7JJ7..:~7!!~^:::^~~~~~~!!?5GB##B!!GGBGGPPY!^.JP?~7YYY&@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@?.~G#!PGG5J7??!~^^^^^^::^!!^.^JP5Y!...~JJ?7~~~~!!!!!7J5GB##BGG!!BGGPPPP5YJJPP5GG5JP&@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@P~&@J5GG5YJJY?!~^^^^^^^~7J:.JB##BGJ: .^JYYYYYJ?77J5PGBGGGPPP5^7PP55555Y55PGGP5PYYGB&@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@Y5@GYBBGPYJYYJ?7!!!~~!?YY.:G####BG5~..^JYY5PPPPP5555555YJJ?~~JYYYYYYYYYY55PGGGGBY5#@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@?#@P#BBGGP5YJ??777?JYY5~.JB##&&##BBY^.:!7?Y5PGGGPP5YJ?7~!77????JJJYYJY555YPB###5?B&@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@&?#&##BGP55YYYYYY5PP5J!:!B##&&&&&#GPY~:::::^~~!!7??777??JJJ?7777??JJJYY55J!J5PGG!Y#@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@&YG#BBP5YYYY5555YJ?!^:^Y##&&&&&##BP5P5?~:::^~!7??????JJJJJJ???7????JYYYYY77?JYJ~7G&@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@#5YJJ7777????????7~:?B##BBBBBBGP5PGGPJ^^~^^~!7JYYYJJJJJJYYJJJ??JJJJJJJYBBBBG@@@@#@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@GYJJ?Y5PP55YJ?7!^~GBP55Y5555J?77777~^~?7!~^^!7?JJ??77?JJJJJ?JJJJJ??JY#&##BY@@@B@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@GYJ?J555YJ?77!~^:!J?!!!!!!!!~:::..:^?PPP5Y?~:^~!!77777?????JJJJJJ???Y#&&##G@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@GJJJJY55YJ?77~^:!JJ?~:^^::::::^^^^!YGGGGPP55J!^:^~!!!!!!777?JJJ?????JP#&&#@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@@BJJ?Y55J?77!^:~?Y5YJJJ?!~~^^^~~!?YPGPP55YYYYYJ!^^^~~~!!~!777???7777J7JG#&@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@@@5??JYYJ7!!~:^?Y5P5YYJJ?7!~~!!J55PPPPYYJ?!!~!7??!~~~~~~~~!777777!!?5?@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//&#&&&&&&&&&&&&&&&&&&&&&&&&&B????J?7!~^^J5YY55YY?JJ?77??JJ?77!!!~~^^:...:~!777!!!!~~~!!7!!!~~@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//BGGGGBGBBBBBBBBBBBGBBGGGGGGGY?77777!~^!J?!!?J?7!~^^^^~~^:.....::::^~^^:^~~~!777!!~~~~!7!!~~:^JB&#########&&&&&&&@@@@@@@@@@@@@
//BPGGGGGGGGGGGGGGGGGGGGGPPPPP5!77!!7!!7?7^:..^:..........:^!?JJJJJJJ??J??7!!~~!!!!~~~~!7!~^7: .^J5PPPPGGGGGGGBBBB###&&&&&@@@@@
//BGGGGGGBGGGGGGGGGGGGPPPP55557.:!!~!7??7!^^^~~!!!!!777777???7!!!!!777???7777!!~!!7!~~~~!~^~?^  .:!5PPPPGGGGGGGBBBBBBBBBBBB##&&
//G555PPPPP555555555YYYYJJJ???~.  ^!~!7?7!~!7???77!~~^^^^^:::^^^^^~~~~!777????777!!!~~!~~^~!?^   ..^?YY55555PPPGGGBBBBBBBGP555B
//PJJYYYYYJJJJJJJJJJ????777!77~.   ^!!~!!!!7??77!!~^^^:::::::::::^^~!?JYYYYJJJJ?7!~^^^~^^~~!?! .  ..:!~~!7?JJJYYY555PPPPGGGGG5P
//5??????????????777777!~^:~7!~.    :~~~~!!7?7777!!!!!!!!!7777?JJY5PGGGP55YJ??77!~^^^^^^^~~!7?. . ...^~..::^~!7??JJJYYYYY555PPG
//Y!7777777!!!!!!!!!!!^.. .!!!!:     .^~~!!7JJJJJYYY5YYYY5555PPPGGGGBGPP55J?7!~~^::^^^^^^~~!77. .. ..:~!....::^^~!!77??JJJJYYYP
//J~!!!~~~~~~!!!!!!~^.   :!!!!!^.     .:^~~!?JJJ55PPP5555P5555PGGPPPP5YY???7~^^::::::^^^~~!!!~.  . ..::~7:...:::::^^~~!!!7????5
//?~~~~~!!77777!!~^:..  ~7!!!!7~:.    ...:::^!7JJY5YY5YYYJJJJJYJYYJJJ??7!~~^:::::::::^^^~~!!!.   ....::^!7:.......::^^^~~^~!!!J
//J!!7777!!!~~~~^^:..  ~7!!!!!!~:.     .......:~!!777777!!!!!!!~~!!~~~~^^^:::::::::^^^~~~~~~~  . ....::^~7?^.......:::^^^^^~~~?
//?~~~~~~~~~~~^^^^... ^!!!!!~~!!^:.     .:.......:^^~^^^^~~^^^^^^^^:::::::::.::::^^^^^^^^^~!^  ...:..::^^~7J!:...:^^^~~~~~~~~~?
//?^^^^^^^~~~^^^^:...:7!!!!!~!!7!^..     .::..  ....:::::::::::::::::::::::::::^^^^~^^^:^!77.    ..:.:::^~!7J7::::^^:::::^^~~~?
//?^^^^^^:^^^^^^:....!!!!!!!!!777~:..     ...:...   ...................:::::^^^^^^^^^^^~!77~     ..:..::^~~!?J?^::::::....:^^~?
//?^^^^^^::^^^^^:..:!!!!!!!!!!777!^:..    ......:...................:::^^^^^^^^^^^^^^~!!!!!:     ..:..:^^~~!7?JJ^:^::::.....:^?
//7^^^^:::^^^^^::::~!!!!!!!!!!777!^::..    .::::...................::::^^^^^^^^^^^~~~~~~~~^.      ..:.::^^~~~!?JJ~^^^^:::.....!
//7^^::^^^^^^^^^^:^7!!!!!!7777777!~^::..    ..:^^^::...............::::::::^^~~~~~~~::^:::.       ..:.:::^~~~:. .^~^^^^^::....!
//?^^^^^^^^^^^^^^^!!!!7!!77777777!~^^::..     ..:^^^^^^^^::::::....::::^^^^^~~~!~~^^::^^::.     ....^:::^^^.       :^~^^^::...~
//7^^^^^^^^^^^^^^!!!!777777777!^:...::::..  .....::^~~!!!~~~^^:::::::::::^^~~~~~~^::^^~^:..      ...^^:^:.          :^~~^^::..~
//7^^^^^^^^^^^^^!!!!777777!~:.        .::.......::^^~~!!!!!~~~^^:::::::::::^^~~^^^^~!!!~:..    ....:^^:             .^^~~^^::.!
//7:^^^:^^^^^^:~!!7777!~:.               .::...:::^^^~~~!!!7!~^^^^::.::::::^^^^^~~7?7?7!^.. ....::::^^              .:^^~^^^::!
//7:::::^^^^^^^77!~^:.                     :^:.:::^^^~~~~~~!!!!~~~~^^^^^^^^~~~^~!77JJJ?!^. ..::.::^!!.               .:^~~^^^:!
//7^:::::^:^^^:::.                          .:^:::^^~~^~~~~~!!!!!!~~!!~^^~~~~~~!!7?J?7~:. .::^^^~!!^.                 .:^~^^^^7
//7^^::::::::::::.                            ...:^^^~^^^^^~~~^^!!!!!!~^~~!~!!7777?7^...:..^~~!~^:.            ..     ..^~~^^^7
//7:^^::.:::::::.. ..                              .:^^^^^^^^^^:~!!!!7!~77777777?7^...:::::~^:.                 ..     .:^^^^^7
//7:^^^:::::::::......                                .:^^^:^~^^^!7!7!777?JYJJ?7~:.:::^::...                    ....  ..:^~~^^7
//7:^^^^::::::::...........                             .::^^^:^~!?777!7??JJ?7!^:::^^^^:::.                     ........:^~~^^7