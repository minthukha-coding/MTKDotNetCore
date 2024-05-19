using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MTKDotNetCore.RestAPIWithNLayer.Features.MyanmarProverbs;

[Route("api/[controller]")]
[ApiController]
public class MyanmarProverbsController : ControllerBase
{
    private async Task<Myanmarproverbs> GetDataFromApi()
    {
        HttpClient httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://raw.githubusercontent.com/sannlynnhtun-coding/Myanmar-Proverbs/main/MyanmarProverbs.json");
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Myanmarproverbs>(jsonStr);
            return model;
        }
        return null;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var model = await GetDataFromApi();
        return Ok(model.Tbl_MMProverbsTitle);
    }

    [HttpGet("{titleName}")]
    public async Task<IActionResult> Get(string titleName)
    {
        var model = await GetDataFromApi();
        var item = model.Tbl_MMProverbsTitle.FirstOrDefault(x => x.TitleName == titleName);
        if (item is null)
        {
            return NotFound();
        }
        var titleId = item.TitleId;
        var lst = model.Tbl_MMProverbs.Where(x => x.TitleId == titleId);
        return Ok(lst);
    }

    [HttpGet("{titleId}/{proverbId}")]
    public async Task<IActionResult> Get(int titleId, int proverbId)
    {
        var model = await GetDataFromApi();
        var item = model.Tbl_MMProverbs.FirstOrDefault(x => x.TitleId == titleId && x.ProverbId == proverbId);

        return Ok(item);
    }
}
public class Myanmarproverbs
{
    public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
    public Tbl_Mmproverbs[] Tbl_MMProverbs { get; set; }
}

public class Tbl_Mmproverbstitle
{
    public int TitleId { get; set; }
    public string TitleName { get; set; }
}

public class Tbl_Mmproverbs
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
    public string ProverbDesp { get; set; }
}
