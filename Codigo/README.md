<div align="center">
  <h1>⚔️ Jogo de Combate por Turnos ("BitaBit")</h1>
  <p><i>Um jogo tático de exploração em mapa de nós e combates em turno desenvolvido na Unity.</i></p>
</div>

<br>

## 📜 Sobre o Projeto

Este projeto é um **jogo de combate tático por turnos**, desenvolvido com foco em planejamento estratégico. Nele, o jogador explora um mapa progressivo (estilo Roguelite de nós) e avança por batalhas de diferentes dificuldades. Durante o combate, o cenário utilizes tiles isométricos onde tanto personagens do jogador quanto NPCs podem executar suas ações e ataques de forma intercalada, criando um fluxo dinâmico de trocação.

## ✨ Principais Funcionalidades

- 🗺️ **Mapeamento em Nós:** Progressão num mapa estilo árvore `Branching Paths`. O jogador navega avaliando as opções de avanço por lutas "Fáceis", "Médias" ou o temido "Chefão".
- ⚔️ **Sistema de Turnos:** Uma mecânica consolidada de seleção aliado-inimigo controlada pelo `ControladorDeSelecao`, garantindo fluidez e responsividade na hora do ataque.
- 📐 **Spawns Dinâmicos e Tilemaps:** Posicionamento de personagens regido por Tilemaps Isométricos, o que possibilita gerações dinâmicas pelo cenário no começo do combate.
- 💻 **Código Base Limpo e Desencapsulado:** Estrutura feita para ser simples e modular, fazendo uso de interfaces, padronização orientada a objetos (OOP) e Scripting em C#.

## 🛠️ Tecnologias Utilizadas

- **Engine:** [Unity](https://unity.com/) (Suporta renderização e lógica do projeto).
- **Linguagem:** C# (.NET / Mono).
- **Controle de Versão:** Git / GitHub.

## 🚀 Como Executar Localmente

### Pré-requisitos
- É necessário ter instalado o [Unity Hub](https://unity3d.com/get-unity/download) e uma versão recente da Unity instalada.

### Rodando o jogo
1. Realize o clone deste repositório para sua máquina local usando:
   ```bash
   git clone https://github.com/SEU-USUARIO/Bit-by-Bit.git
   ```
2. Abra o **Unity Hub**, vá até a aba "Projects", e clique em **Open**.
3. Navegue até a pasta `Codigo/BitaBit` onde clonou o projeto e a selecione.
4. Após o Unity carregar as dependências e abrir a interface (pode levar alguns minutos na primeira vez), navegue até a janela **Project**.
5. Em `Assets/Cenas`, abra a cena de **Mapa** (ou Inicializadora) e pressione o botão **Play** (▶) acima da Game View para iniciar o fluxo.

## 🤝 Contribuições

Como este projeto é um marco de portfólio já **concluído**, ele não se encontra mais em desenvolvimento ativo pelo autor original. No entanto, o código é amigável e aberto a quem desejar fazer _fork_ e experimentar coisas novas, adicionar mecânicas de habilidades ou aprimorar os gráficos!

---
<div align="center">
  <sub>Criado com ☕ por Felipe. Aberto para fins educacionais e de demonstração.</sub>
</div>