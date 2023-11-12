using Data_Access.IRepositorios;
using Domain.DTO;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Newtonsoft.Json;
using System.Text.Json;

namespace Servicios.Servicios
{
    public class ServicioPais : IServicioPais
    {

        private IRepositorioPais _repoPais;
        public ServicioPais(IRepositorioPais repoPais)
        {
            _repoPais = repoPais;
        }

        public PaisDTO Add(PaisDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(PaisDTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PaisDTO> GetAll()
        {
            List<PaisDTO> res = new List<PaisDTO>();
            IEnumerable<Pais> Paises = _repoPais.GetAll();
            foreach (Pais p in Paises)
            {
                PaisDTO paisDTO = new PaisDTO(p);
                res.Add(paisDTO);
            }
            return res;
        }

        public PaisDTO GetPais(int id)
        {
            PaisDTO res = new PaisDTO(_repoPais.GetPais(id));
            return res;
        }

        public async Task<IEnumerable<PaisDTO>> LoadPaisesAsync()
        {
            IEnumerable<PaisDTO> paisesDTO = await Load();
            foreach(PaisDTO paisDTO in paisesDTO)
            {
                try
                {
                    Pais pais = new Pais(paisDTO);
                    pais.PaisId = 0;
                    IEnumerable<Pais> paises = _repoPais.GetAll();
                    

                    if(_repoPais.GetByName(pais.Nombre) == null)
                    {
                        _repoPais.Add(pais);
                        _repoPais.Save();

                    }

                    

                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return paisesDTO;
        }

        private static readonly HttpClient client = new HttpClient();
        public async Task<IEnumerable<PaisDTO>> Load()
        {
            try
            {
                string url = "https://restcountries.com/v2/all";

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    List<PaisDTO> paises = new List<PaisDTO>();
                    JsonElement rootElement = doc.RootElement;
                    if (rootElement.ValueKind == JsonValueKind.Array)
                    {
                        foreach (JsonElement element in rootElement.EnumerateArray())
                        {
                            PaisDTO pais = new PaisDTO
                            {
                                PaisId = 0,
                                Nombre = element.GetProperty("name").GetString(),
                                Codigo = element.GetProperty("alpha3Code").GetString()
                            };
                            paises.Add(pais);
                        }
                    }

                    return paises;

                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Error al cargar los datos: " + e.Message);
            }
        }
    }
}


