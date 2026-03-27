
/*
namespace WebTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService ChatService;

//ne treba moyda idk bas
        public ChatController(IChatService chatService)
        {
            ChatService = chatService;
       
        }

        // --- CHAT SEKCIJA ---

        [HttpPost("PosaljiPoruku")]
        public async Task<IActionResult> SendMessage(int sId, int rId, int dId, [FromBody] string text)
        {
            try
            {
                var Poruka = await ChatService.PosaljiPorukuAsync(sId, rId, dId, text);
                return Ok(Poruka);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("ChatIstorija/{u1}/{u2}/{dId}")]
        public async Task<IActionResult> GetHistory(int u1, int u2, int dId)
        {
            try { return Ok(await ChatService.GetHistoryChatAsync(u1, u2, dId)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

     
    }*/
