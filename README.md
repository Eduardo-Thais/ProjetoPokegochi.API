### APIs do projeto Pokegochi
APIs do meu projeto que estou fazendo deu um pequeno jogo de pokemon usando a API do PokeAPI e um sistema de login para manter a evolução usando o Identity do AspNetCore <br>
##

Registra usuario
```
[POST](/api/ApiUsuario/cadastro)
{
  "username": "string",
  "dataNascimento": "2025-10-02T22:38:10.404Z",
  "password": "string",
  "confirmPassword": "string"
}
```
Loga usuario retornando um JWT
```
[POST](/api/ApiUsuario/login)
{
  "username": "string",
  "password": "string"
}
```
Retorna uma lista de Pokemons
```
Precisa aplicar o token JWT no header da requisição para retornar com sucesso
[GET](/api/Mascote)
```
Retorna os dados do Pokemon com a ID especificada
```
Precisa aplicar o token JWT no header da requisição para retornar com sucesso
[GET](/api/Mascote/id)
```
