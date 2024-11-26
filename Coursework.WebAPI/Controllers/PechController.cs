using Coursework.Application.Services;
using Coursework.Core.Abstractions;
using Coursework.Core.Models;
using Coursework.WebAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coursework.WebAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class PechController : ControllerBase
    {
        private readonly IPechService _pechService;
        public PechController(IPechService pechService)
        {
            _pechService = pechService;
        }

        [HttpPut("{index:int},{firstItem:float},{lastItem:float},{stepItem:float}")]
        public async Task<ActionResult<List<List<float>>>> GetPechRange(int index, float firstItem, float lastItem, float stepItem, [FromBody] PechRequest responseRange)
        {
            if (index < 0 || index > 7) return BadRequest("The index is out of range");
            float[] resArray = new float[]
            {
                responseRange.tPech,
                responseRange.height,
                responseRange.tNach,
                responseRange.kTeplo,
                responseRange.Teplo,
                responseRange.p,
                responseRange.tPov,
                responseRange.kTeploOtd
            };
            List<List<float>> res = new List<List<float>>();
            res.Add(new List<float>());
            res.Add(new List<float>());
            res.Add(new List<float>());
            for (float i = firstItem; i < lastItem; i += stepItem)
            {
                resArray[index] = i;
                res[0].Add(i);
                res[1].Add(Pech.CalculateVremNagr(
                    resArray[0],
                    resArray[1],
                    resArray[2],
                    resArray[3],
                    resArray[4],
                    resArray[5],
                    resArray[6],
                    resArray[7]));
                res[2].Add(Pech.CalculateTKon(
                    resArray[0],
                    resArray[1],
                    resArray[2],
                    resArray[3],
                    resArray[4],
                    resArray[5],
                    resArray[6],
                    resArray[7]));
            }
            return Ok(res);
        }

        [HttpPost("{index:int},{firstItem:float},{lastItem:float},{stepItem:float}")]
        public async Task<ActionResult<Guid>> CreatePechRange(int index, float firstItem, float lastItem, float stepItem, [FromBody] PechRequest responseRange)
        {
            if (index < 0 || index > 7) return BadRequest("The index is out of range");
            float[] resArray = new float[]
            {
                responseRange.tPech,
                responseRange.height,
                responseRange.tNach,
                responseRange.kTeplo,
                responseRange.Teplo,
                responseRange.p,
                responseRange.tPov,
                responseRange.kTeploOtd
            };
            List<Guid> guids = new List<Guid>();
            for (float i = firstItem; i < lastItem; i += stepItem)
            {
                resArray[index] = i;
                var (pech, error) = Pech.Create(
                Guid.NewGuid(),
                resArray[0],
                resArray[1],
                resArray[2],
                resArray[3],
                resArray[4],
                resArray[5],
                resArray[6],
                resArray[7]);
                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(error);
                }
                var pechId = await _pechService.CreatePech(pech);

                guids.Add(pechId);
            }

            return Ok(guids[0]);
        }

        [HttpGet]
        public async Task<ActionResult<List<PechResponse>>> GetPech()
        {
            var pech = await _pechService.GetAllPech();

            var response = pech.Select(b => new PechResponse(b.Id, b.TPech, b.Height, b.TNach, b.KTeplo, b.Teplo, b.P, b.TPov, b.KTeploOtd, 
                Pech.CalculateVremNagr(b.TPech, b.Height, b.TNach, b.KTeplo, b.Teplo, b.P, b.TPov, b.KTeploOtd),
                Pech.CalculateTKon(b.TPech, b.Height, b.TNach, b.KTeplo, b.Teplo, b.P, b.TPov, b.KTeploOtd)));

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePech([FromBody] PechRequest request)
        {
            var (pech, error) = Pech.Create(
                Guid.NewGuid(),
                request.tPech,
                request.height,
                request.tNach,
                request.kTeplo,
                request.Teplo,
                request.p,
                request.tPov,
                request.kTeploOtd);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var pechId = await _pechService.CreatePech(pech);

            return Ok(pechId);
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdatePech(Guid id, [FromBody] PechRequest request)
        {
            var bookId = await _pechService.UpdatePech(id, 
                request.tPech,
                request.height,
                request.tNach,
                request.kTeplo,
                request.Teplo,
                request.p,
                request.tPov,
                request.kTeploOtd);
            return Ok(bookId);
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeletePech(Guid id)
        {
            return Ok(await _pechService.DeletePech(id));
        }
    }
}
