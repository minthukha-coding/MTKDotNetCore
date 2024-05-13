using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MTKDotNetCore.RestAPIWithNLayer.Features.LaHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaHtaukBayDinController : ControllerBase
    {
        private LaHtaukBayDin _data;

        private async Task<LaHtaukBayDin> GetDataAsync()
        {
            string jsonstr = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<LaHtaukBayDin>(jsonstr);
            return model;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> Questions()
        {
            var model = await GetDataAsync();
            return Ok(model.questions);
        }

        #region Model

        public class LaHtaukBayDin
        {
            public Question[] questions { get; set; }
            public Answer[] answers { get; set; }
            public string[] numberList { get; set; }
        }

        public class Question
        {
            public int questionNo { get; set; }
            public string questionName { get; set; }
        }

        public class Answer
        {
            public int questionNo { get; set; }
            public int answerNo { get; set; }
            public string answerResult { get; set; }
        }

        #endregion

    }
}
