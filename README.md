<div align="center">

# 🔍 LeadSemSite API

<p>
  API desenvolvida em <strong>ASP.NET</strong>, integrada à API do <strong>Serper</strong>, para buscar empresas no Google e identificar potenciais clientes que não possuem site — facilitando a prospecção e captação de novos leads.
</p>

<p>
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET"/>
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#"/>
  <img src="https://img.shields.io/badge/Serper%20API-4285F4?style=for-the-badge&logo=google&logoColor=white" alt="Serper API"/>
  <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" alt="Swagger"/>
</p>

</div>

---

## 💻 Projeto

API para encontrar empresas sem site no Google Maps. Basta informar um termo de busca e uma cidade para listar os leads, com opção de filtrar apenas empresas sem site e paginar os resultados — ideal para prospecção e captação de novos clientes.

## ✨ Funcionalidades

- ✅ Busca de empresas no Google Maps por termo e cidade
- ✅ Identificação automática de empresas que não possuem site
- ✅ Filtro para listar apenas empresas sem site
- ✅ Paginação dos resultados
- ✅ Retorno de dados detalhados do lead (contato, endereço, avaliação, localização)
- ✅ Documentação interativa via Swagger

## 🚀 Recursos Utilizados

- `ASP.NET`
- `C#`
- `Serper API` (busca no Google)
- `Swagger`

## 🗺️ Endpoint

### Leads

| Método | Rota                                   | Descrição                               |
|--------|-----------------------------------------|--------------------------------------------|
| GET    | `/Leads/consultar-empresas-sem-site`    | Busca empresas no Google Maps por termo e cidade |

**Parâmetros de busca:**

| Parâmetro       | Tipo    | Descrição                                      |
|-----------------|---------|--------------------------------------------------|
| `termo`         | string  | Termo de busca (ex: "Barbearia")                 |
| `cidade`        | string  | Cidade onde a busca será realizada                |
| `apenasSemSite` | boolean | Filtra apenas empresas que não possuem site       |
| `pagina`        | int     | Número da página de resultados                    |

**Exemplo de resposta:**

```json
{
  "cidade": "Lagoa Santa, MG",
  "termo": "Barbearia",
  "pagina": 1,
  "temMaisPaginas": true,
  "total": 9,
  "leads": [
    {
      "id": "18184137437564806571",
      "nome": "Barbearia Gomes",
      "endereco": "R. Conde Dolabela, 1429 - Várzea, Lagoa Santa - MG, 33400-000, Brasil",
      "telefone": "+55 31 99835-2657",
      "categoria": "Barbearia",
      "avaliacao": 5,
      "totalAvaliacoes": 161,
      "urlImagemEmpresa": "https://exemplo.com/imagem.jpg",
      "latitude": -19.6385857,
      "longitude": -43.8851408,
      "possuiSite": false
    }
  ]
}
```

## 📸 Screenshot

<div align="center">
  <img width="1892" height="907" alt="image" src="https://github.com/user-attachments/assets/62a07e54-af94-4ed3-b6b1-21e46dc09c7d" />
</div>

## 👤 Autor

**Arthur Souza**

<p>
  <a href="https://github.com/Arthursouzafut22"><img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white"/></a>
  <a href="inkedin.com/in/arthur-souza-588168256/"><img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white"/></a>
</p>

---

<div align="center">
  Feito com 💜 por Arthur Souza
</div>
