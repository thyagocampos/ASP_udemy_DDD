using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.Serviceprovider;
        }

        [Fact(DisplayName ="CRUD de Usuário")]
        [Trait("CRUD","User entity")]
        public async Task E_possivel_realizar_CRUD_usuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                if (context != null)
                {
                    UserImplementation _repositorio = new UserImplementation(context);
                    UserEntity _entity = new UserEntity
                    {
                        Email = "teste@email.com",  
                        Name = "Usuário teste"
                    };

                    var _registroCriado = await _repositorio.InsertAsync(_entity);

                    Assert.NotNull(_registroCriado);
                    Assert.Equal("teste@email.com", _registroCriado.Email);
                    Assert.Equal("Usuário teste", _registroCriado.Name);
                    Assert.False(_registroCriado.Id == Guid.Empty);


                }
            }
        }
    }
}

