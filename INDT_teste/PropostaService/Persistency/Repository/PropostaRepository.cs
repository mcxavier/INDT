using Azure;
using Microsoft.EntityFrameworkCore;
using PropostaService.Core.DTO;
using PropostaService.Core.Interfaces;
using PropostaService.Persistency.Contexts;
using PropostaService.Persistency.Entities;
using System;
using System.ComponentModel;
using System.Reflection;

namespace PropostaService.Persistency.Repository
{
    public enum StatusPropostas
    {
        [Description("Em Análise")]
        EmAnalise = 1,
        [Description("Aprovada")]
        Aprovada = 2,
        [Description("Rejeitada")]
        Rejeitada = 3,
        [Description("Contratada")]
        Contratada = 4,  
    }


    public class PropostaRepository :IPropostaRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public PropostaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseResponseDTO<PropostaDTO>> GetPropostaById(int id)
        {
            var proposta = await _dbContext.Propostas.AsNoTracking()
                                                     .FirstOrDefaultAsync(f => f.id == id);

            if (proposta == null)
            {
                return new BaseResponseDTO<PropostaDTO>
                {
                    statusCode = 404,
                    ErrorMessage = "Registro não encontrado"
                };
            }

            var propostaDTO = new PropostaDTO()
            {
                id = proposta.id,
                descrciao = proposta.descricao,
                statusId = proposta.status,
                status = GetEnumDescription((StatusPropostas)proposta.status),
            };


            return new BaseResponseDTO<PropostaDTO>
            {
                statusCode = 200,
                Data = propostaDTO,
            };
        }

        public async Task<BaseResponseDTO<PropostaDTO>> UpdateProposta(PropostaDTO record)
        {
            var proposta = await _dbContext.Propostas.FirstOrDefaultAsync(f => f.id == record.id);

            if (proposta == null)
            {
                return new BaseResponseDTO<PropostaDTO>
                {
                    statusCode = 404,
                    ErrorMessage = "Registro não encontrado"
                };
            }

            // só contrata proposta aprovada (2)
            if ((proposta.status != 2) && (record.statusId == 4))
            {
                return new BaseResponseDTO<PropostaDTO>
                {
                    statusCode = 409,
                    ErrorMessage = "Proposta não pode ser Contratada."
                };
            }
            else
            {
                proposta.dataContratacao = DateTime.Now;
            }

                proposta.descricao = record.descrciao;
            proposta.status = record.statusId;


            await _dbContext.SaveChangesAsync();

            return new BaseResponseDTO<PropostaDTO>
            {
                statusCode = 200,
                Data = record
            };
        }

        public async Task<BaseResponseDTO<PropostaDTO>> InsertProposta(PropostaDTO record)
        {
            var proposta = new Proposta();

            proposta.descricao = record.descrciao;
            proposta.status = record.statusId;

            _dbContext.Propostas.Add(proposta);
            await _dbContext.SaveChangesAsync();

            return new BaseResponseDTO<PropostaDTO>
            {
                statusCode = 201,
                Data = record,
            };
        }


        public async Task<Tuple<List<PropostaDTO>, int>> ListPropostasByParams(Dictionary<string, string?> queryStringDictionary)
        {
            var query = _dbContext.Propostas.AsQueryable();

            query = query.OrderByDescending(w => w.id);

            var total = await query.CountAsync();

            var lista = await query.AsNoTracking().ToListAsync();

            var result = new List<PropostaDTO>();

            foreach (var proposta in lista)
            {
                result.Add(new PropostaDTO()
                {
                    id = proposta.id,
                    descrciao = proposta.descricao,
                    statusId = proposta.status,
                    status = GetEnumDescription((StatusPropostas)proposta.status),
                });
            }

            return new Tuple<List<PropostaDTO>, int>(result, total);
        }


        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
