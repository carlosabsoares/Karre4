A API ControleLancamento foi contruida da seguinte forma:

	* net 8 - Devido ser uma versão de suporte de longo tempo pela microsoft e ser a mais recente lançada;
	
	* Sqlite - Foi escolhido esse banco pela sua portabilidade e devido a sua criação ser um arquivo com final .db e o banco é executado de forma local;
	
	* EntityFramework - Foi escolhido esse ORM para que o banco de dados ficasse "desacoplado", facilitando uma migração de banco bastando alterar o drive de conexão. Além disso, facilita a interação com a base de dados, podendo utilizar o LINQ para interações com a base e sem precisar "re-escrever" nunhuma instrução junto ao banco ;
	
	* Migration - Foi escolhido para que a base seja criada / atualizada de forma automátizada;
	
	* CRQS - Foi utilizada essa "arquitetura" visando dar maior agilidade a aplicação e podendo até termos a utilização de um banco NoSQL para efetuação das consultas e um banco relacional para a persistência dos dados. Em alguns casos, essa arquitetura é utilizada ( como num e-commerce, por exemplo ) para agilizar o carregamento de informações na tela pelo front ( teria um processo rodando em backend que atualiza o banco NoSql com as informações persistidas pelo banco relacional ou podemos usar um serviço de fila (RabbitMQ ou ServiceBus, por exemplo) para que esse sincronismo seja feito );
	
			No caso do CQRS utilizado as seguintes classse:  
			
				* Query - Classe que utilizamos para "iniciar" os orquestradores ( Handler ) de pesquisas ( querys ). Foi utilizada a bibliotecar Flunt.Br para efetuar as validações das informações enviadas pelo controller;
				
				* Command - Classe que utilizamos para "iniciar" os orquestradores ( Handler ) de comandos na aplicação ( insert, update, deletes, processamentos ). Foi utilizada a bibliotecar Flunt.Br para efetuar as validações das informações enviadas pelo controller;
				
				* Handler - Classe que permite que possamos "orquestras" as ações e retornar para o controller. usamos o conceito de "Fail fast". Utilizamos o método Validate da classe Query ou command logo no inicio da execução do handle, para que caso tenha algum problema de validação, ocorra o erro logo e fique mais rápido a execução do sistema, já que não fará interações com bancos, com api's externas e etc.
				
			
	* Mediator - Utilizado para que baste acionar a classe Query ou Command e o mediator consegue localizar o handler que utiliza essa classe e iniciar o processamento correto;
	
	* Xunit - Utilizado para efetuar os testes unitários da api. Escolhido por ser o mais utilizado na plataforma Microsoft, incluisive sendo recomendado pela empresa;
	
Para a execução do serviço, basta executa-lo, pois irá aplicar a criação do banco de dados autimaticamente, pois foi configurado para que o migration execute na subida da aplicação e caso não tenha o banco ou nenhuma alteração aplicada no banco, será atualizado.

	

	Temos 2 controller:
	
	LancamentoController ( onde será efetuado as operações de lançamento ), com os métodos:
		* /v1/Lancamento/getAll - Obtem todos os lançamentos efetuados;
		* /v1/Lancamento - Cadastra um lançamento. O Json deve ser esse:
			{
			  "valor": 0, 
			  "tipoOperacao": 1
			} 
			onde o valor é o valor monetário e o tipoOperacao é o tipo de operação realizada ( 1 - Débito e 2 - Crédito )
		* ​/v1​/Lancamento​/{id} - Deleta um lançamento. Ao deletar o lançamento, o registro anterior será ajustado;
		
	ConsolidadoController ( onde será retornado as informações consolidadas por dia ), com os métodos:
		* /v1/Consolidado/getAll - Retorna todos os consolidado por data
		* /v1/Consolidado/getByData - Retorna o consolidado por uma data em expecífico.


	Para executarmos a API, basta selecionar o projeto ControleLancamentoKKarre4.Api.Api como o projeto Inicial ("StartUp project") e iniciar o projeto.

	
