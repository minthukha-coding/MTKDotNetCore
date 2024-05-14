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
            string jsonstr = await System.IO.File.ReadAllTextAsync("LaHtaukBayDin.json");
            var model = JsonConvert.DeserializeObject<LaHtaukBayDin>(jsonstr);
            return model;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> Questions()
        {
            var model = await GetDataAsync();
            return Ok(model.questions);
        }

        [HttpGet("numberList")]
        public async Task<IActionResult> NumberList()
        {
            var model = await GetDataAsync();
            return Ok(model.numberList);
        }

        [HttpGet("{questionNo}/{no}")]
        public async Task<IActionResult> Answer(int questionNo, int no)
        {
            var model = await GetDataAsync();
            return Ok(model.answers.FirstOrDefault(x => x.questionNo == questionNo && x.answerNo == no));
        }

        #region Model

        public class LaHtaukBayDin
        {
            public Question[] questions { get; set; }
            public AnswerCS[] answers { get; set; }
            public string[] numberList { get; set; }
        }

        public class Question
        {
            public int questionNo { get; set; }
            public string questionName { get; set; }
        }

        public class AnswerCS
        {
            public int questionNo { get; set; }
            public int answerNo { get; set; }
            public string answerResult { get; set; }
        }
    }

    #endregion

}
