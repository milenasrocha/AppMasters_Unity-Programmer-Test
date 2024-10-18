Projeto de seleção de dev Unity

3 Partes:
- 1ª: Ler os dados a partir de um JSON como se fosse uma api
- 2ª: Algumas telas tipo um form/wizard/onboarding
- 3ª: Exibir os resultados baseado no que ele selecionou nessas telas anteriores

1 - Tela collection:
- Título no topo/centro
- Botão de next no canto superior direito
- Exibir uma grid de cards no centro da tela, basedo no array do json collections
- Usuário pode clicar em um card para selecionar uma das collections, ao fazer isso, a tela guarda qual ID do card selecionado
- O botão de next fica desabilitado até o usuario selecionar um card

2 - Tela de item:
- Título no topo/centro
- Botão de next no canto superior direito
- Botão de voltar no canto superior esquerdo, o usuário pode voltar pra tela collection, podendo visualizar o que foi selecionado anteriormente, como também trocar a seleção e continuar
- Exibir uma grid de cards no centro da tela, baseado no array do json area/autograph/imprint, se o usuário escolheu "area" na tela collection, só exibir o conteudo do json "area", e assim por diante
- Usuário pode clicar em um card para selecionar um dos itens, ao fazer isso, a tela guarda qual ID do card selecionado
- O botão de next fica desabilitado até o usuario selecionar um card

3 - Tela de tamanhos:
- Título no topo/centro
- Botão de next no canto superior direito
- Botão de voltar no canto superior esquerdo, o usuário pode voltar pra tela item, podendo visualizar o que foi selecionado anteriormente, como também trocar a seleção e continuar
- Input de número: "Largura" em centímetros (width)
- Input de número: "Profundidade" em centímetros (depth)

4 - Tela final:
- Exibir uma caixa no centro, com as dimensões fornecidas na tela de tamanhos. se o usuário digitou 20x20, então exibir uma caixa de 20 por 20, se o usuário digitou 20x10, exibir uma caixa de 20x10
- No canto superior esquerdo, exibir qual a collection selecionada, se o usuario tiver selecionado o id "area", exibir o texto "Area collection", e assim vai
- No canto superior direito, exibir qual item selecionado, se o usuario tiver selecionado o id "area-01", exibir o texto "Area Item 01", e assim vai

Bonus, importante deixar pra fazer esses por último:
- Exibir uma imagem no card, ao invés de somente fazer um botão com texto(por enquanto pode ser qualquer imagem, o que vale é o exemplo)
- Não usar escala para dimensionar o cubo, mas sim, gerar uma mesh
- Tornar um app

Importante considerar:
- Na hora de realizar o teste, irei testar com json diferentes, usando valores diferentes. Onde o texto pode ter mais/menos caracteres pra exibir no card, ou onde podem ter mais ou menos de 3 elementos, talvez 5 collections, talvez 1, talvez 10 items, é importante que o app consiga ler o json, e exibir o dado que está nesse json, de forma que se trocarmos o json com mais/menos dados(obviamente, mantendo os mesmos campos), ele continue funcionando
- Não precisa ter a interface linda, perfeita, mas não pode quebrar nada
- Código reutilizável é melhor
- Criar o projeto utilizando URP

Prazo máximo de 10 horas a partir do recebimento dessa explicação.