using Coelsa.Common;
using Coelsa.Models;
using Coelsa.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Coelsa.WebApi.Controllers
{
    [Route("api/contacts")]
    [ApiController]

    public class ContactController : Controller
    {
        private readonly IContactService _ContactService;

        public ContactController(IContactService contactService)
        {
            this._ContactService = contactService;
        }
        [HttpGet("test")]
        public async Task<ActionResult<Response<string>>> Test()
        {
            return Ok(
                    HttpCustomeResult<string>.ResponseOK("Test OK", null)
                );
        }

        [HttpPost()]
        public async Task<ActionResult<Response<int>>> InsertContact([FromBody] ContactModel contact)
        {
            try
            {
                var result = await _ContactService.InsertContact(contact);
                return Ok(
                        HttpCustomeResult<int>.ResponseOK(result, Constants.MSG_CONTACT_INSERTED)
                    );
            }
            catch (FunctionalException fex)
            {
                return BadRequest(
                        HttpCustomeResult<int>.ResponseBusinessError(fex.Message)
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        HttpCustomeResult<int>.ResponseApplicationError(ex.Message)
                    );
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<int>>> UpdateContact(int id, [FromBody] ContactModel contact)
        {
            try
            {
                var result = await _ContactService.UpdateContact(id, contact);
                return Ok(
                        HttpCustomeResult<int>.ResponseOK(result, Constants.MSG_CONTACT_UPDATED)
                    );
            }
            catch (FunctionalException fex)
            {
                return BadRequest(
                        HttpCustomeResult<int>.ResponseBusinessError(fex.Message)
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        HttpCustomeResult<int>.ResponseApplicationError(ex.Message)
                    );
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<int>>> DeleteContact(int id)
        {
            try
            {
                var result = await _ContactService.DeleteContact(id);
                return Ok(
                        HttpCustomeResult<int>.ResponseOK(result, Constants.MSG_CONTACT_DELETED)
                    );
            }
            catch (FunctionalException fex)
            {
                return BadRequest(
                        HttpCustomeResult<int>.ResponseBusinessError(fex.Message)
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        HttpCustomeResult<int>.ResponseApplicationError(ex.Message)
                    );
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ContactModel>>> GetContact(int id)
        {
            try
            {
                var result = await _ContactService.GetContact(id);
                return Ok(
                        HttpCustomeResult<ContactModel>.ResponseOK(result, null)
                    );
            }
            catch (FunctionalException fex)
            {
                return BadRequest(
                        HttpCustomeResult<int>.ResponseBusinessError(fex.Message)
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        HttpCustomeResult<int>.ResponseApplicationError(ex.Message)
                    );
            }
        }

        [HttpGet()]
        public async Task<ActionResult<Response<IEnumerable<ContactModel>>>> GetAllContact()
        {
            try
            {
                var result = await _ContactService.GetAllContact();
                return Ok(
                        HttpCustomeResult<IEnumerable<ContactModel>>.ResponseOK(result, null)
                    );
            }
            catch (FunctionalException fex)
            {
                return BadRequest(
                        HttpCustomeResult<int>.ResponseBusinessError(fex.Message)
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        HttpCustomeResult<int>.ResponseApplicationError(ex.Message)
                    );
            }
        }

        [HttpGet("{company}/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<Response<IEnumerable<ContactModel>>>> GetAllContact(string company, int pageNumber , int pageSize)
        {
            try
            {
                var result = await _ContactService.GetContactsByCompanyAsync(company, pageNumber, pageSize);
                return Ok(
                        HttpCustomeResult<IEnumerable<ContactModel>>.ResponseOK(result, null)
                    );
            }
            catch (FunctionalException fex)
            {
                return BadRequest(
                        HttpCustomeResult<int>.ResponseBusinessError(fex.Message)
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        HttpCustomeResult<int>.ResponseApplicationError(ex.Message)
                    );
            }
        }
    }
}
