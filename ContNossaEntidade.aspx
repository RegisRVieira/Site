<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Conteudo.Master" CodeBehind="ContNossaEntidade.aspx.cs" Inherits="Site.ContNossaEntidade" %>

<asp:Content ContentPlaceHolderID="CpLateral" runat="server">
    <script type="text/javascript" src="Js/Apoio.js"></script>
    <form id="frmNossaEntidade" runat="server">
        <div id="menuLateralNE" class="menuflutuaNE">
            <ul>
                <li>
                    <asp:LinkButton ID="lbAAsu" runat="server" Text="A ASU" OnClick="ativaASU"></asp:LinkButton></li>
                <li><a href="ContNossasPessoas.aspx">Nossas "Pessoas"</a></li>
                <!--<li><asp:LinkButton ID="lbSede" runat="server" Text="Sub Sede Lageado" OnClick="ativaSedeLageado"></asp:LinkButton></li>-->
                <li>
                    <asp:LinkButton ID="lbDap" runat="server" Text="Departamento de Aposentados" OnClick="ativaDap"></asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbClube" runat="server" Text="Clube de Campo" OnClick="ativaClube"></asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbEstatuto" runat="server" Text="Estatuto Social" OnClick="nossoEstatuto"></asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbAuxilioFuneral" runat="server" Text="Regimento Auxilio Funeral" OnClick="nossoRegimento"></asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbBalancete" runat="server" Text="Prestação de Contas" OnClick="nossoBalancete"></asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbJornal" runat="server" Text="Jornal" OnClick="nossoJornal"></asp:LinkButton></li>
                <li id="treeline-icon" class="treeline-icon" onclick="AcessarNossaEntidade()">&#9776;</li>
                <li id="treeline-NossaEntidade" class="treeline-closeicon" onclick="FecharNossaEntidade() ">&cross;</li>
            </ul>
        </div>
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="cpContent" runat="server">
    <asp:MultiView ID="mwConteudo" runat="server">
        <asp:View ID="vwASU" runat="server">
            <div style="margin: 0; padding: 0">
                <%# montarConteudo(31) %>
            </div>
        </asp:View>
        <asp:View ID="vwSedeLageado" runat="server">
            <div style="margin: 0; padding: 0">
                <%# montarConteudo(63) %>
            </div>
        </asp:View>
        <asp:View ID="vwDap" runat="server">
            <div style="margin: 0; padding: 0">
                <%# montarConteudo(62) %>
            </div>
        </asp:View>
        <asp:View ID="vwClube" runat="server">
            <main>
                <%# montarConteudo(61) %>
            </main>
        </asp:View>
        <asp:View ID="vwEstatuto" runat="server">
            <style>
                ul li {                    
                    list-style: none;
                }
            </style>
            <div style='width: 870px; height: 900px; overflow-y: scroll; padding-right: 15px;'>

                <p style="width: 100%; text-align: center; font-size: 28px; font-weight: 700;">ESTATUTO SOCIAL</p>
                <br />

                <div>
                    <p><b>CAPÍTULO 1</b></p>
                    <p><b>Da Denominação, Fins, Sede e Foro.</b></p><br />

                    <p><b>Artigo 1º</b> – A Associação dos Servidores da Universidade Estadual Paulista “Júlio de Mesquita Filho” – UNESP, Campus de Botucatu, fundada em 08 de julho de 1.967, denominada aqui simplesmente “ASU”, será regida pelo presente Estatuto e pela legislação que lhe for aplicável.</p><br />

                    <p><b>Artigo 2º</b> – A ASU é uma entidade com personalidade jurídica própria sem fins lucrativos, com Sede, Administração e Foro no Campus Universitário da Unesp de Botucatu/SP, Distrito de Rubião Júnior S/N, Estado de São Paulo, sendo a mesma instituída com prazo indeterminado.</p><br />

                    <p><b>Parágrafo único</b> – a ASU poderá criar sub-sedes em outros locais do município de Botucatu e Região, para melhor atendimento de seu quadro associativo.</p><br />
                    <p><b>Artigo 3º</b> – A ASU tem por finalidades</p>
                    <ul>                        
                        <li>I-	promover a união de seus associados e dependentes, por meio de atividades sociais, culturais, recreativas, esportivas e outras mais que venham a atingir objetivos de interesse comuns;</li>
                        <li>II-	desenvolver, dentro das disponibilidades de seus recursos orçamentários e financeiros, o seu patrimônio social, visando sempre ao interesse da Entidade e de seu quadro associativo;</li>
                        <li>III-	criar condições, inclusive financeiras, para que ocorram a troca de idéias, conhecimentos e experiências, não só entre os associados, mas também, com as demais associações de classe, através da realização de Congressos, Simpósios, Palestras, etc;</li>
                        <li>IV-	promover campanhas de esclarecimento aos associados sobre assuntos de importância aos mesmos;</li>
                        <li>V-	colaborar com as autoridades constituídas, Associações de Classe e Entidades Sociais, naquilo que venha trazer benefícios à sociedade como um todo e à ASU em particular;</li>
                        <li>VI-	criar e gerenciar os mais diversos artigos de convênios, desde que ocorra um interesse mútuo entre a ASU e seus conveniados;</li>
                        <li>VII-	patrocinar (dentro das disponibilidades de seus recursos orçamentários e financeiros) e defender os interesses e as causas justas dos associados, quando solicitada, ficando expressamente autorizada a representá-los individual ou coletivamente, em juízo ou não e em quaisquer circunstâncias, nos termos do permitido no Inciso XXI do artigo 5º, da Constituição da República Federativa do Brasil;</li>
                        <li>VIII-	propiciar aos seus associados e dependentes, na medida de suas possibilidades, melhores condições de vida, principalmente no que concerne aos problemas relacionados com a proteção à saúde e a outros benefícios de ordem pessoal e social.</li>
                    </ul>
                    <b>Artigo 4º</b> – É vedado à ASU:</b>
                    <ul>                    
                        <li><b>I-</b>	dedicar-se a fins políticos-partidários e religiosos;</li>
                        <li><b>II-</b>	apoiar ou combater candidatos a cargos políticos;</li>
                        <li><b>IIIV-</b>	participar de movimentos que conflitem com o presente Estatuto.</li>
                    </ul>

                </div><br />
                <div>
                    <b>CAPÍTULO II</b><br />
                    <p><b>Do Quadro Social</b></p><br />

                    <p><b>Artigo 5º</b> – Serão admitidos como sócios e titulares todos os servidores da Unesp – Campus de Botucatu, funcionários das Fundações ligadas às Unidades do Campus de Botucatu, funcionários da ASU, funcionários das demais Associações ligadas ao Campus de Botucatu, funcionários que prestam serviços não eventuais às Unidades do Campus de Botucatu e pensionistas, viúvas ou viúvos de servidores da Unesp de Botucatu, compreendendo os associados da Secretaria de Estado da Saúde, regiões de Botucatu e Bauru, associados da FAMESP, qualquer que seja a região e pessoas físicas que possuem contrato vigente com a ASU.</p><br />
                    <p><b>Artigo 6º</b> – A ASU possui as seguintes categorias de sócios:</p>
                    <ul>                    
                        <li><b>I-</b>	Fundadores: aqueles sócios que assinaram a Ata de Fundação da ASU, em 08 de julho de 1.967;</li>
                        <li><b>II-</b>	Contribuintes: todos aqueles que, após aceita sua filiação, recolhem contribuição conforme estabelece o presente Estatuto;</li>
                        <li><b>III-</b>	Beneméritos: aqueles que, pertencentes ou não ao quadro social, tenham prestado relevantes serviços à ASU;</li>                    
                    </ul>
                    
                    <p><b>Parágrafo único</b> – a concessão de títulos para os sócios citado no inciso III deverá ser aprovada pela Diretoria Executiva.</p><br />

                    <p><b>Artigo 7º</b> – O beneficiado do título citado no inciso III está isento de contribuições, podendo freqüentar as atividades e dependências da Entidade, estando, porém vetado a votar ou ser votado nos processos eleitorais da ASU e de usufruír dos convênios mantidos pela Entidade, excluindo os regularmente inscritos como sócios.</p>

                    <p><b>Artigo 8º</b> – Todos os sócios das diversas categorias citadas nos incisos do artigo 6º deverão  cumprir, rigorosamente, o presente Estatuto.</p>

                    <p><b>Artigo 9º</b> – Será excluído do quadro Associativo da ASU todo sócio citado no artigo 5º que perdeu vínculo empregatício com as instituições ali elencadas. </p><br />

                    <p><b>Parágrafo único</b> – o Servidor da Unesp que passar para o quadro de inativos terá seu direito adquirido e preservado como sócio.</p><br />

                    <p><b>Artigo 10</b> – Serão considerados dependentes do Sócio Titular, para todos os efeitos legais, estando os mesmos sujeitos às normas estabelecidas pelo presente Estatuto:</p>
                    <ul>                        
                        <li>I-	esposo (a) </li>
                        <li>II-	filhos (as) solteiros (as) até 24 anos;</li>
                        <li>III-	pensionista;</li>
                        <li>IV-	filhos de qualquer idade, se portadores de deficiências físicas ou mentais, comprovação médica;</li>
                        <li>V-	pai e mãe;</li>
                        <li>VI-	filhos (as) adotivos (as), desde que com comprovação judicial, respeitando-se os limites de idade citados no inciso II;</li>
                        <li>VII-	companheiro (a) desde que apresente documento comprobatório registrado em Cartório;</li>
                        <li>VIII-	tutelado (a) e curatelado (a), desde que apresente documento comprobatório;</li>
                        <li>IX-	enteados solteiros (as) até 24 anos;</li>
                        <li>X-	netos (as) solteiros (as) até 24 anos. </li>
                        <li>XI-	Agregados: filhos casados até 24 anos, filhos, netos e enteados acima de 24 anos, sogro, sogra genro e nora.</li>                    
                    </ul>
                    <p><b>Parágrafo único</b> – dos agregados, que se tornarem dependentes do associado, será cobrada mensalidade, cujo valor e condições deverão ser aprovados através de Assembleia Geral, ficando definido que os seus valores deverão ser reajustados em consonância com o parágrafo segundo, do artigo 12, deste estatuto.</p><br />
                </div><br />
                <div>
                    <p><b>CAPÍTULO III</b></p>
                    <p><b>Da Admissão de Sócios</b></p>

                    <p><b>Artigo 11</b> – São condições necessárias para se ingressar ao quadro associativo da ASU:</p>
                    <ul>                    
                        <li>I-	preencher os requisitos constantes do artigo 5º do presente Estatuto;</li>
                        <li>II-	encaminhar  proposta devidamente preenchida e assinada à secretaria da ASU;</li>                                
                    </ul>

                    <p><b>Parágrafo único</b> – após avaliação da proposta de admissão pela Diretoria Executiva da Entidade, esta poderá:</p><br />
                    <ul>                    
                        <li>I-	recusar o pedido  de admissão, informando por escrito ao   interessado e alegando os motivos da recusa;</li>
                        <li>II-	aceitar o pedido, comunicando ao interessado para se apresentar à secretaria para apresentação de toda documentação exigida e tomar ciência das condições em que a proposta foi aceita.</li>                
                    </ul>
                    
                    <b>Artigo 12</b> – A ASU poderá praticar as seguintes modalidades de recebimento:
                    <ul>                                      
                        <li>I - taxa de admissão;</li>
                        <li>II - mensalidade;</li>
                        <li>III - anuidade</li>                
                    </ul>
                
                    <p>§ 1º - Os respectivos valores de que trata o caput deste Artigo serão analisados e aprovados em Assembleia Geral.</p>

                    <p>§ 2º - Os valores a que se referem o parágrafo anterior serão reajustados automaticamente, todo mês de maio com base no índice do IPCA-IBGE do Estado de São Paulo ou outro que venha substituí-lo.</p>
                </div><br />
                <div>
                    <p><b>CAPÍTULO IV</b></p>
                    <p><b>Da Demissão e Readmissão de Sócios</b></p>

                    <p><b>Artigo 13</b> – Ao sócio será facultado o direito de pedir exclusão do quadro associativo, desde que sua situação financeira esteja regularizada com a ASU.</p>
  
                    <p><b>Artigo 14</b> – Após o pedido de exclusão, automaticamente o interessado terá suspensos todos os direitos e demais vantagens presentes neste Estatuto.</p>
 
                    <p><b>Artigo 15</b> – No ato do pedido de exclusão, o interessado deverá fazer a devolução de sua carteira de associado, a de seus dependentes, bem como de outras carteiras que sejam vinculadas a convênios.</p>

                    <p><b>Artigo 16</b> – Após a perda do associado de seu vínculo empregatício com as instituições elencadas no artigo 5º, o mesmo deverá obrigatoriamente comunicar o fato à ASU e promover todas as medidas legais e/ou estatutárias visando a seu desligamento do quadro associativo da Entidade. </p><br />

                    <p><b>Parágrafo único</b> – caso o sócio não cumpra o que determina o Caput deste artigo, a Diretoria Executiva da ASU deverá tomar todas as medidas legais visando resguardar os interesses da Entidade.</p><br />
                </div><br />
                <div>
                <b>CAPÍTULO V</b>
                    <p><b>Dos Direitos e vantagens dos sócios</b></p>                    
                    <p><b>Artigo 17</b> – São direitos dos sócios:-</p>
                    <ul>
                        <li>I-	Freqüentar todas as dependências da ASU e participar das diversas atividades promovidas pela mesma;</li>
                        <li>II-	Participar das Assembleias, com direito a voz e voto;</li>
                        <li>III-	Recorrer junto à Diretoria Executiva sobre decisões tomadas que julgar prejudiciais a si ou à própria Entidade;</li>
                        <li>IV-	Encaminhar sugestões, visando aos interesses da ASU;</li>
                        <li>V-	Encaminhar requerimento, solicitando que a Diretoria Executiva convoque Assembleia Geral Extraordinária, conforme estabelecido no inciso II do artigo 39;</li>
                        <li>VI-	Votar nas eleições da Entidade, desde que possua, no mínimo, 6 (seis) meses de filiação no dia do pleito eleitoral;</li>
                        <li>VII-	Ser votado nas eleições da Entidade, desde que possua, no mínimo, 2 (dois) anos de filiação no dia do pleito eleitoral;</li>
                        <li>VIII-	Ter acesso a toda documentação, seja financeira ou de Secretaria, da ASU, porém, sendo vetada a sua retirada da sede da Associação;</li>
                        <li>IX-	Usufruir de toda infra-estrutura disponível na ASU, relacionada a Departamentos, Assessorias, Comissões, etc;</li>
                        <li>X-	Utilizar todos os convênios firmados pela Entidade, respeitando limites e normas propostas para seu funcionamento, desde que se encontre em gozo de seus direitos.</li>
                    </ul>
                </div><br />
                <div>
                    <p><b>CAPÍTULO VI</b></p>
                    <p><b>Dos Deveres dos Sócios</b></p>

                    <p><b>Artigo 18</b> – São Deveres dos Sócios:</p>
                    <ul>
                        <li>I-	Cumprir as disposições contidas neste Estatuto, nos Regulamentos, Regimentos e Normas a serem estabelecidas, bem como acatar as decisões da Assembleia Geral e da Diretoria Executiva;</li>
                        <li>II-	Quitar pontualmente todos os seus compromissos com a ASU;</li>
                        <li>III-	Desempenhar de maneira exemplar os cargos ou funções que lhe forem confiados;</li>
                        <li>IV-	Portar-se de maneira correta em todos os eventos promovidos pela ASU;</li>
                        <li>V-	Tratar com cordialidade os membros da Diretoria Executiva e seus colegas associados;</li>
                        <li>VI-	Participar de maneira assídua das Assembleias previamente convocadas pela Diretoria Executiva;</li>
                        <li>VII-	Colaborar para que a ASU obtenha um crescimento social e patrimonial cada vez mais constante;</li>
                        <li>VIII-	Zelar pelo patrimônio da ASU;</li>
                        <li>IX-	Comunicar à Secretaria da ASU quando ocorrer mudança de endereço, estado civil, nascimento ou adoção de filhos ou desvinculação empregatícia com as instituições mencionadas no artigo 5º;</li>
                        <li>X-	Não exercer nas dependências da Entidade qualquer movimento de caráter político–partidário, religioso ou que caracterize discriminação racial;</li>
                        <li>XI-	Não permitir, em hipótese alguma, que sua carteira de sócio e/ou dependente, bem como a de Convênio, seja utilizada por terceiros, ficando sujeito às penalidades previstas no presente Estatuto, Regulamento e Normas da ASU.</li>                                    
                        </ul>
                </div><br />
                <div>
                    <p><b>CAPÍTULO VII</b></p>
                    <p><b>Do Código de Processo Disciplinar</b></p>

                    <p><b>Artigo 19</b> – Serão aplicadas as penas tipificadas no Estatuto da Unesp bem como serão garantidos os princípios constitucionais de ampla defesa àquele que vier a descumprir o presente Estatuto.</p>
                </div><br />
                <div>
                    <p><b>CAPITULO VIII</b></p>
                    <p><b>Do Convênio</b></p><br />
                    <p><b>SEÇÃO I</b></p>
                    <p><b>Do Contrato</b></p>
                    <p><b>Artigo 20</b> – Todo e qualquer convênio e/ou promoção entre a ASU e a empresa deverá ser regido por contrato.</p>
                    <p>§ 1º - entende-se como convênio o contrato celebrado entre a ASU e a pessoa física e/ou jurídica que fornece produtos ou prestam serviços ao associado, mediante pagamento.</p>
                    <p>§ 2º - ao efetuar o pagamento ao fornecedor, a ASU sub-roga-se no direito de cobrar do associado o respectivo valor, acrescido de despesas administrativas, encargos, juros, correção monetária e outros, desde que fixados pela Diretoria Executiva;</p>
                    <p><b>Artigo 21</b> – As cláusulas contratuais deverão ser estipuladas pela Diretoria Executiva bem como a relação jurídica entre o associado e a empresa conveniada;</p>
                    <p><b>Artigo 22</b> – Compete ao Presidente da ASU ou seu substituto legal assinar o referido contrato;</p>
                    <p><b>Artigo 23</b> – À ASU é reservado o direito de rescindir o contrato em qualquer momento, sem qualquer ônus para a mesma, quando o conveniado não cumprir as cláusulas contratuais.</p><br />

                    <p><b>SEÇÃO II</b></p>
                    <p><b>Do pagamento</b></p>
                    <p><b>Artigo 24</b> – O associado autoriza a ASU a proceder ao pagamento do seu débito e de seus dependentes mediante desconto em folha de pagamento ou débito em sua conta corrente, independentemente de notificação ou aviso.</p>
                    <p><b>Artigo 25</b> – O associado que não efetuar o pagamento de seus débitos e de seus dependentes na data do vencimento, ficará com os seus direitos estatutários e benefícios automaticamente suspensos, independentemente de notificação ou aviso, sendo proibida a utilização das respectivas credenciais.</p>
                    <p>§ 1º - o associado restabelecerá seu direito estatutário e benefícios ao quitar seu débito e de seus dependentes, mediante avaliação da Diretoria Executiva e consulta aos órgãos de proteção ao crédito, exceto se o fizer judicialmente, caso em que estará automaticamente excluído do quadro social.</p>
                    <p>§ 2º - ficará suspenso o associado que utilizar o sistema de convênio até quitação final de seu débito e de seus dependentes ressalvado os demais direitos estatutários.</p>
                    <p><b>Artigo 26</b> – Os débitos contraídos pelo sócio e de seus dependentes deverão ser atualizados de acordo com o artigo 389 do Código Civil.</p>
                </div><br />
                <div>
                <b>CAPÍTULO IX</b>
                    <p><b>Do Patrimônio Social</b></p>

                    <p><b>Artigo 27</b> – O patrimônio da ASU será constituído por:</p>
                    <ul>
                        <li>I-	Móveis e utensílios, ações, imóveis adquiridos, ou doações;</li>
                        <li>II-	Rendas arrecadadas mensalmente ou anualmente.</li>
                    </ul>
                    
                    <p><b>Artigo 28</b> – Um levantamento dos bens pertencentes à ASU será feito, obrigatoriamente, anualmente, procedendo-se ao seu lançamento no Livro Próprio.</p>
                    <p><b>Artigo 29</b> – Todos os bens imóveis e móveis adquiridos pela ASU, assim como todos aqueles transferidos à Associação por terceiros, ou que se perderem, ou que se substituírem, serão de maneira obrigatória lançados em Livros Próprios. No caso de edificações, o seu lançamento em livro só ocorrerá anualmente, devendo constar de modo pormenorizado a descrição da obra.</p>
                </div><br />
                <div>
                    <p><b>CAPÍTULO X</b></p>
                    <p><b>Das Receitas e das Despesas</b></p>

                    <p><b>Artigo 30</b> – Constituirão receitas da ASU:</p>
                    <ul>
                        <li>I-	contribuições de sócios;</li>
                        <li>II-	aluguéis das dependências da Entidade;</li>
                        <li>III-	indenizações;</li>
                        <li>IV-	donativos;</li>
                        <li>V-	 rendimentos e aplicações financeiras;</li>
                        <li>VI-	taxas e recolhimentos;</li>
                        <li>VII-	jóias;</li>
                        <li>VIII-	rendas de eventos;</li>
                        <li>IX-	publicidade;</li>
                        <li>X-	outras fontes de recursos.</li>
                    </ul>

                    <p><b>Artigo 31</b> – Constituirão despesas da ASU:</p>
                    <ul>
                        <li>I-	salários e encargos dos funcionários;</li>
                        <li>II-	aquisição de materiais de consumo;</li>
                        <li>III-	custeio e conservação de bens;</li>
                        <li>IV-	serviços diversos;</li>
                        <li>V-	aquisição de bens móveis e imóveis;</li>
                        <li>VI-	outras despesas necessárias, previamente autorizadas dentro do orçamento.</li>                        
                    </ul>
                    
                    
                </div><br />
                <div>
                    <p><b>CAPÍTULO XI</b></p>
                    <p><b>Dos Órgãos da Entidade</b></p>

                    <p><b>Artigo 32</b> – São órgãos deliberativo, executivo e fiscalizador da Associação dos Servidores da Unesp Campus de Botucatu – ASU, respectivamente:</p>
                    <ul>
                        <li>I-	Assembleia Geral</li>
                        <li>II-	Diretoria Executiva</li>
                        <li>III-	Conselho Fiscal</li>                    
                    </ul>
                    <p><b>SEÇÃO I</b></p>
                    <p><b>ASSEMBLEIA GERAL</b></p>

                    <p><b>Artigo 33</b> – A Assembleia Geral é o órgão máximo da ASU, com competência para deliberar e decidir sobre todos os assuntos previstos ou não neste Estatuto.</p>
                    <p><b>Artigo 34</b> – A Assembleia Geral Ordinária, sempre que necessário, será convocada pelo Presidente da ASU, através de edital, com local, data, horário e ordem do dia definidos, que será afixado na sua sede, em murais, em outros veículos de comunicação da ASU e em jornais locais.</p>
                    <p><b>Artigo 35</b> – A Assembleia Geral Ordinária deliberará em primeira Convocação com 50% dos sócios, ou, não se atingindo esse número, em segunda Convocação, trinta minutos após a primeira, com qualquer número de sócios.</p>

                    <p><b>Artigo 36</b> – À Assembleia Geral compete:</p>
                    <ul>
                        <li>I-	Deliberar sobre os assuntos para os quais foi convocada;</li>
                        <li>II-	Resolver sobre a dissolução da Associação;</li>
                        <li>III-	Encaminhar solução para a destinação da Diretoria Executiva ou parte dela;</li>
                        <li>IV-	Decidir sobre a compra e/ou venda de imóveis;</li>
                        <li>V-	Avaliar a demonstração financeira (Balancete), que deverá ser apresentada pelo tesoureiro e/ou Conselho Fiscal;</li>
                        <li>VI-	Eleger os integrantes da Diretoria Executiva e do Conselho Fiscal;</li>
                        <li>VII-	Destituir os integrantes da Diretoria Executiva e do Conselho Fiscal;</li>
                        <li>VIII-	Aprovar contas;</li>
                        <li>IX-	Alterar estatuto.</li>
                    </ul>

                    <p><b>Artigo 37</b> – Para as deliberações a que se referem os incisos IX e XI, é exigido o voto concorde de dois terços dos presentes à Assembleia especialmente convocada para esse fim, não podendo ela deliberar, em primeira convocação, sem a maioria absoluta dos associados, ou com menos de um terço nas convocações seguintes:</p>

                    <p><b>Artigo 38</b> – A Assembleia Geral Extraordinária será realizada nas seguintes condições:</p>
                    <ul>
                    <li>I-	Quando o Presidente da Associação assim o entender, por se tratar de assunto de extrema urgência;</li>
                    <li>II-	Quando o sócio enviar à Diretoria Executiva da ASU requerimento explicitando o assunto que justifique a convocação Extraordinária da Assembleia, juntando ao referido requerimento 10% de assinaturas do quadro social, devendo constar número de matrícula, nome e assinatura dos mesmos.</li>
                    </ul>

                    <p><b>Artigo 39</b> – A Assembleia Geral Extraordinária somente será instalada e funcionará:</p>
                    <ul>
                    <li>I-	No caso do Inciso I, do artigo anterior, respeitando-se o que consta do Capítulo XI, Seção I.</li>
                    <li>II-	No caso do Inciso II do artigo anterior, quando em 1º Convocação com 70% do número de assinantes do requerimento ou, em 2º-chamada, com os mesmos 70% do número de assinantes do requerimento, caso não se atinja a quantidade determinada neste caso, a avaliação do mérito do pedido ficará prejudicada e será arquivada.</li>
                    </ul>

                    <p><b>Artigo 40</b> – As decisões da Assembleia, seja Ordinária ou Extraordinária, obedecerão aos critérios de 50% +1 dos presentes, exceto no caso de destituição de Diretoria Executiva ou extinção da ASU que deverá obedecer ao artigo 38;</p>
                    <p><b>Artigo 41</b> – Todos os casos omissos, que porventura não encontrem respaldo neste Estatuto, serão resolvidos através das decisões tomadas nas Assembleias Gerais Ordinárias ou Extraordinárias, respeitando-se as regras contidas nos artigos do Capítulo XI, Seção I, podendo-se, tais decisões, formar jurisprudência para os casos futuros e que sejam idênticos. </p>
                    <p><b>Artigo 42</b> – A Assembleia Geral, seja Ordinária ou Extraordinária, será sempre presidida pelo presidente da ASU e, no caso de seu impedimento, pelo vice-presidente da Entidade. </p>
                    <p><b>Artigo 43</b> – As atas de preenchimento de demais formalidades ficarão a cargo do 1º Secretário e, no impedimento deste, do 2º Secretário.</p>
                    <p><b>Artigo 44</b> – No caso de extinção da Associação, a Assembleia Geral decide a qual Entidade Beneficente serão destinados bens e saldos financeiros, se porventura existirem, devendo todos os presentes assinarem a Ata dessa Assembleia.</p><br />
                    <p><b>Parágrafo único</b> – os trâmites para que se discutam a extinção ou não da Associação, obedecerão ao que consta do caput do artigo 38.</p><br />
                    <p><b>Artigo 45</b> – No caso de ocorrer a destituição da Diretoria Executiva, no transcorrer da própria Assembleia, os sócios deverão tomar duas medidas:</p>
                    
                    <ul>
                        <li>I-	Nomear interinamente, através de votação, os novos Coordenadores e seus respectivos cargos, até que ocorram novas eleições;</li>
                        <li>II-	Marcar, no prazo de 90 dias após a decisão da Assembleia, a novas eleições e calendário eleitoral, obedecendo às disposições do Capítulo XII do presente estatuto.</li>
                    </ul>

                    <p><b>SEÇÃO II</b></p>
                    <p><b>DIRETORIA EXECUTIVA</b></p>

                    <p><b>Artigo 46</b> – A Diretoria  Executiva da ASU terá a seguinte composição:</p>

                    <ul>
                        <li>I-	Presidente</li>
                        <li>II-	Vice-Presidente</li>
                        <li>III-	1º Secretário</li>
                        <li>IV-	2º Secretário</li>
                        <li>V-	1º Tesoureiro</li>
                        <li>VI-	2º Tesoureiro</li>
                        <li>VII-	Departamento de Aposentados</li>
                        <li>VIII-	Departamento de Clube de Campo</li>
                        <li>IX-	Departamento Sócio Cultural</li>
                        <li>X-	Departamento Patrimonial</li>
                        <li>XI-	Departamento de Divulgação e Relações Públicas</li>
                        <li>XII-	Departamento de Convênios</li>
                        <li>XIII-	Departamento Jurídico</li>
                        <li>XIV-	Departamento de Esportes</li>
                        <li>XV-	Departamento de Assistência Social</li>
                        <li>XVI-	Departamento de Informática</li>
                    </ul>
                    
                    <p><b>Parágrafo único</b> – os Departamentos citados nos incisos VII a XVI, serão compostos por coordenadores e vice-coordenadores.</p><br />

                    <p><b>Artigo 47</b> – São atribuições da Diretoria Executiva:</p>
                    <ul>
                        <li>I-	dirigir e administrar a ASU, em todas as suas áreas, utilizando-se dos dispositivos estabelecidos neste Estatuto;</li>
                        <li>II-	elaborar, apresentar à plenária da Assembleia e executar o plano semestral ou anual das atividades de cada exercício, conforme disponibilidades orçamentárias e financeiras;</li>
                        <li>III-	autorizar recebimentos e despesas;</li>
                        <li>IV-	acompanhar e aprovar, quando for o caso, o balancete da tesouraria;</li>
                        <li>V-	zelar pelo patrimônio da entidade;</li>
                        <li>VI-	discutir e deliberar sobre todos os assuntos de sua competência, remetendo para a Assembleia aqueles que julgar necessários;</li>
                        <li>VII-	ratificar ou mesmo retificar ou anular atos da própria Diretoria Executiva se a maioria entender conveniente;</li>
                        <li>VIII-	fazer programações envolvendo os diversos departamentos, deliberando sobre as realizações dos mesmos e proceder a devida fiscalização;</li>
                        <li>IX-	nomear membros nos diversos departamentos, se julgar necessário;</li>
                        <li>X-	convocar reuniões, ordinárias ou extraordinárias, marcando dia, hora, local e pauta, cuja convocação será enviada com   48 horas de antecedência;</li>
                        <li>XI-	convocar Assembleia, ordinária ou extraordinária, quando o assunto assim requerer, obedecendo aos critérios estabelecidos nos diversos Artigos e Parágrafos do Capítulo XI, Seção I;</li>
                        <li>XII-	nomear, quando for o caso, novos membros para cargos, com exceção do vice-presidente, para completar o término do mandato;</li>
                        <li>XIII-	propor a reforma ou modificação deste Estatuto;</li>
                        <li>XIV-	elaborar regulamentos, regimentos, portarias ou demais normas, baixando-as através do presidente;</li>
                        <li>XV-	autorizar assinatura de contratos de locações, concessão e aquisição de bens;</li>
                        <li>XVI-	designar representante(s) da ASU para participar de atos para os quais foi convidada, e que seu presidente não possa comparecer;</li>
                        <li>XVII-	apresentar mensalmente o balancete da ASU em murais e anualmente através de Assembleia Geral e pelo Jornal Oficial da entidade;</li>
                        <li>XVIII-	conceder, quando houver consenso, títulos, medalhas e prêmio a quem o merecer;</li>
                        <li>XIX-	resolver os casos omissos em sua área de competência;</li>
                        <li>XX-	atender às solicitações  do Conselho Fiscal; </li>
                        <li>XXI-	gerenciar toda e qualquer parte administrativa da ASU;</li>
                        <li>XXII-	Propor à Assembleia Geral a criação de novos departamentos e se aprovado nomear o coordenador e vice, interinos, até a realização da próxima eleição;</li>
                        <li>XXIII-	Propor à Assembleia Geral a criação de subdivisões administrativas que venham a facilitar a estrutura de diretoria e departamentos obedecendo-se sempre à hierarquia funcional;</li>
                    </ul>

                    <p><b>Artigo 48</b> – São atribuições do Presidente:</p>
                    <ul>
                        <li>I-	dirigir e coordenar todas as atividades da Associação, para que a mesma atinja seus objetivos;</li>
                        <li>II-	representar a ASU em juízo ou fora dele;</li>
                        <li>III-	presidir as reuniões da Diretoria Executiva e Assembleias Gerais, Ordinárias ou Extraordinárias;</li>
                        <li>IV-	convocar reuniões da Diretoria Executiva e as Assembleias;</li>
                        <li>V-	assinar, de modo solidário, com o 1º Tesoureiro ou seu substituto, os documentos orçamentários, título de créditos, cheques e ordens de pagamento;</li>
                        <li>VI-	cumprir e fazer cumprir o que consta deste Estatuto e das deliberações da Assembleia Geral e da própria Diretoria Executiva;</li>
                        <li>VII-	admitir funcionários para a ASU, bem como promover a demissão, quando julgar conveniente para a Entidade, ouvindo sempre a Diretoria Executiva;</li>
                        <li>VIII-	orientar, supervisionar e promover reuniões com os Coordenadores dos Departamentos, para que os objetivos propostos por estes possam ser alcançados;</li>
                        <li>IX-	despachar toda a rotina diária da Entidade, orientando os funcionários em suas atribuições.</li>
                    </ul>

                    <p><b>Artigo 49</b> – São atribuições do Vice- Presidente:</p>
                    <ul>
                        <li>I-	Exercer a presidência da Associação nas faltas, licenças e impedimentos do presidente, obedecendo aos dispositivos constantes deste Estatuto;</li>
                        <li>II-	quando o presidente estiver em exercício, o vice-presidente deverá auxiliá-lo em todos os assuntos administrativos da entidade sempre que solicitado.</li>
                    </ul>

                    <p><b>Artigo 50</b> – São atribuições do 1º Secretário:</p>
                    <ul>
                        <li>I-	secretariar Assembleias, assim como reuniões de Diretoria Executiva,  redigindo e subscrevendo atas;</li>
                        <li>II-	supervisionar todo o trabalho de secretaria, como despacho de ofícios, papéis, convocações, correspondências,cadastramento, emissão de carteiras de sócios e demais atividades pertinentes à secretaria;</li>
                        <li>III-	ter sob sua responsabilidade todo o expediente da secretaria da Associação;</li>
                        <li>IV-	preparar o relatório anual do movimento social e apresentá-lo à Diretoria Executiva, bem como auxiliar a tesouraria na elaboração de seu relatório;</li>
                        <li>V-	ter sob sua guarda todos os livros e papéis relacionados à Secretaria.</li>
                    </ul>

                    <b>Artigo 51</b> – Compete ao 2º Secretário:
                    <ul>
                        <li>I-	substituir o 1º secretário nos casos de faltas, licença e impedimento do mesmo;</li>
                        <li>II-	quando o 1º secretário estiver em exercício, o 2º secretário deverá auxiliá-lo na sua área de competência.</li>
                    </ul>

                    <p><b>Artigo 52</b> – Compete ao 1º Tesoureiro:</p>
                    <ul>
                        <li>I-	gerir a arrecadação de despesas;</li>
                        <li>II-	efetuar o pagamento de despesas previamente autorizadas, assinando, com o presidente, cheques, ordens de pagamentos, títulos e demais documentos necessários;</li>
                        <li>III-	disponibilizar na página da ASU na internet os balancetes mensais, trimestrais e anuais, e em forma impressa na sede da Associação para consulta do associado;</li>
                        <li>IV-	supervisionar e dirigir todos os trabalhos pertinentes à tesouraria;</li>
                        <li>V-	ter sob sua guarda e responsabilidade todos os livros de registro e demais documentos comprobatórios referentes à sua área de atuação;</li>
                        <li>VI-	manter em dia o cadastro financeiro, econômico e patrimonial da Associação.</li>
                    </ul>
                    <p><b>Artigo 53</b> – Compete ao 2º Tesoureiro:</p>
                    <ul>
                        <li>I-	substituir o 1º Tesoureiro nos casos de faltas, licenças e impedimentos do mesmo;</li>
                        <li>II-	auxiliar, na área de atuação, o 1º Tesoureiro, mesmo quando este estiver em exercício. </li>
                    </ul>
                    <p><b>Artigo 54</b> – São atribuições dos Departamentos:</p>
                    <ul>
                        <li>I-	promover e programar os mais variados tipos de eventos dentro de sua área de atuação;</li>
                        <li>II-	manter  relacionamento constante com a Diretoria Executiva da ASU, levando até a mesma todas as informações de suas atividades;</li>
                        <li>III-	promover, como objetivo principal, a integração e união entre os associados da ASU;</li>
                        <li>IV-	Divulgar os eventos, bem como o nome do Departamento e da  ASU.</li>
                        <li>V-	Reunir-se periodicamente, sempre que julgar necessário, para discutir assuntos de interesses do Departamento;</li>
                        <li>VI-	Efetuar prestação de contas de suas atividades, mensalmente, à ASU.</li>
                    </ul>
                    <p><b>Artigo 55</b> – Compete aos Coordenadores de Departamentos:</p>
                    <ul>
                        <li>I-	supervisionar todas as áreas de atuação de seu departamento;</li>
                        <li>II-	presidir reuniões entre os membros pertencentes ao  Departamento;</li>
                        <li>III-	participar, como Coordenador do Departamento, das reuniões ordinárias ou extraordinárias convocadas pela Diretoria Executiva;</li>
                        <li>IV-	participar das Assembleias Gerais;</li>
                        <li>V-	nomear comissões de apoio para desenvolver atividades dentro do Departamento;</li>
                        <li>VI-	manter relacionamento constante  com a Diretoria Executiva da ASU, deixando-a a par de tudo que está ocorrendo no Departamento;</li>
                        <li>VII-	realizar e encaminhar, mensalmente, a apresentação de contas do Departamento à Diretoria Executiva da ASU;</li>
                        <li>VIII-	representar o Departamento ou indicar substituto, quando convidado oficialmente para os atos e/ou eventos;</li>
                        <li>IX-	exercer os demais atos pertinentes ao cargo.</li>
                    </ul>

                    <p><b>Parágrafo único</b> – em caso de impedimento ou vacância do coordenador, assumirá o Departamento o vice-coordenador.</p><br />

                    <p><b>SEÇÃO III</b></p>
                    <p><b>CONSELHO FISCAL</b></p>

                    <p><b>Artigo 56</b> – O Conselho Fiscal é composto por 03 (três) membros titulares e 03 (três) membros suplentes.</p>
                    <p><b>Artigo 57</b> – A composição do Conselho Fiscal será a seguinte: Os três mais votados serão os membros titulares e os três seguintes serão os suplentes.</p><br />
                    <p><b>Parágrafo único</b> – em caso de empate entre candidatos, eleger-se-á aquele que contar com maior tempo de filiação junto à ASU.</p><br />
                    <p><b>Artigo 58</b> – Os componentes do Conselho Fiscal têm mandato de 02 (dois) anos, sendo permitida uma reeleição aos ocupantes dos cargos.</p>
                    
                    <p><b>Artigo 59</b> – Compete ao Conselho Fiscal:</p>
                    <ul>
                        <li>I-	examinar as demonstrações financeiras e os livros contábeis da ASU bem como as contas e os demais aspectos econômico-financeiros;</li>
                        <li>II-	apresentar à Assembleia Geral parecer sobre os negócios e operações em exercício;</li>
                        <li>III-	lavrar em livro de atas e pareceres o resultado dos exames procedidos;</li>
                        <li>IV-	acusar as irregularidades eventualmente verificadas, sugerindo medidas saneadoras.</li>
                    </ul>

                    <p><b>Artigo 60</b> – O Conselho Fiscal reunir-se-á mensalmente, ou a qualquer tempo, desde que convocado.</p>
                    <p><b>Artigo 61</b> – As reuniões Ordinárias do Conselho fiscal serão convocadas por seu presidente e deliberadas por maioria simples dos membros.</p>
                    <p><b>Artigo 62</b> – As reuniões Extraordinárias do Conselho Fiscal serão convocadas pelo presidente ou por maioria de seus membros.</p>
                    <p><b>Artigo 63</b> – O Conselho Fiscal poderá solicitar reunião com a Diretoria Executiva da ASU se julgar necessário.</p>
                    <p><b>Artigo 64</b> – Tanto nas reuniões como nas deliberações, o Conselho Fiscal deverá contar com a presença mínima de 2/3 de seus membros.</p>
                    <p><b>Artigo 65</b> – Na ausência do membro titular do Conselho Fiscal, o suplente será convocado para a reunião.</p>
                    <p><b>Artigo 66</b> – No caso de o Conselho Fiscal detectar irregularidades ou não ser atendido em suas solicitações pela Diretoria Executiva, deverá juntar documentos pertinentes, ou relacionar os faltantes e apresentar à Assembleia Geral.</p>
                </div><br />
                <div>
                <p><b>CAPITULO XII</b></p>
                    <p><b>Das Reuniões</b></p>

                    <p><b>Artigo 67</b> – As reuniões ordinárias da Diretoria Executiva deverão ser realizadas mensalmente ou a critério da diretoria e as extraordinárias, sempre que o assunto demandar urgência.</p>
                    <p><b>Artigo 68</b> – As convocações aos membros da Diretoria Executiva deverão ser feitas com, pelo menos, 48 (quarenta e oito) horas de antecedência, devendo as mesmas conter expediente, ordem do dia, data, horário e local.</p>
                    <p><b>Artigo 69</b> – São considerados membros da Diretoria Executiva todos os que exercem cargos citados no artigo 46 (Seção II – Diretoria Executiva).</p>
                    <p><b>Artigo 70</b> – Para que possam ocorrer as reuniões, deve-se respeitar o quorum de 50% de presença dos convocados, conforme determinado nos artigos 69 e 70.</p>
                    <p><b>Artigo 71</b> – Os assuntos colocados em votação serão considerados aprovados se obtiverem maioria simples dos membros presentes.</p>
                    <p><b>Artigo 72</b> – O presidente da reunião terá, além do seu voto, o direito de utilizar o voto de minerva para decidir o assunto em caso de empate.</p>
                </div><br />
                <div>
                    <p><b>CAPITULO XIII</b></p><br />
                    <p><b>Das Eleições</b></p>

                    <p><b>Artigo 73</b> – As eleições para a escolha da Diretoria Executiva citada na Seção II do Capítulo XI serão realizadas a cada 02 (dois) anos, com antecedência mínima de 30 (trinta) dias do término do mandato da Diretoria Executiva anterior.</p><br />

                    <p><b>Parágrafo único</b> – o presidente da Diretoria Executiva da ASU terá direito a concorrer a apenas uma reeleição. Caso reeleito, não poderá, para o mandato seguinte à reeleição, ocupar os cargos mencionados nos incisos II e V do artigo 46;</p><br />

                    <p><b>Artigo 74</b> – As eleições para a escolha do Conselho Fiscal citado na Seção III do Capítulo XI serão realizadas a cada 02 (dois) anos, com antecedência mínima de 30 (trinta) dias do término do mandato do Conselho anterior.</p>
                    <p><b>Artigo 75</b> – As eleições para a Diretoria Executiva serão feitas através de chapas, devendo constar os nomes dos concorrentes e seus respectivos cargos, bem como a denominação da referida chapa.</p><br />

                    <p><b>Parágrafo único</b> – os sócios concorrentes às eleições deverão apresentar autorização por escrito sobre sua participação na chapa, sendo proibida a vinculação a dois ou mais cargos ou inscrições em duas chapas.</p><br />

                    <p><b>Artigo 76</b> – As eleições para Conselho Fiscal terão inscrições individuais para concorrer ao pleito, devendo os candidatos protocolar as mesmas na secretaria da entidade.</p>
                    <p><b>Artigo 77</b> – As chapas que concorrerão à Diretoria Executiva e os candidatos ao Conselho Fiscal deverão inscrever-se na Secretaria da Associação, obedecendo-se ao calendário eleitoral previamente determinado e divulgado.</p>
                    <p><b>Artigo 78</b> – As inscrições terão um período de 5 (cinco) dias úteis e as eleições serão realizadas após 15 (quinze) dias do encerramento das inscrições.</p>
                    <p><b>Artigo 79</b> – <span style="text-decoration:line-through;"> A Secretaria da entidade deverá divulgar o Edital das Eleições através da imprensa escrita, além da fixação em murais da ASU, com antecedência mínima de 30 (trinta) dias da realização da mesma, estabelecendo data, horário e locais de votação.</span></p>
                    <p><mark> – Para o inicio do processo eleitoral, a secretaria da entidade deverá divulgar o Edital das Eleições através da imprensa escrita ou virtual, sitio,  redes sociais ou em murais mantidos pela ASU, com antecedência mínima de 30 (trinta) dias da sua realização, fixando a data e horário das inscrições.</mark></p>
                    <p><b>Artigo 80</b> – Se houver necessidade, a Associação poderá determinar mais de um local de votação ou urna itinerante.</p>
                    <p><b>Artigo 81</b> – A votação será secreta, não se permitindo o voto por procuração.</p>
                    <p><b>Artigo 82</b> – A ASU escolherá, de comum acordo com as chapas inscritas, a Comissão Eleitoral, que será composta por 01(um) presidente, 01(um) secretário e 03 (três) membros responsáveis pelas eleições e apuração logo após o pleito.</p>
                    <p><b>Artigo 83</b> – Cada chapa inscrita poderá indicar junto à Comissão Eleitoral os nomes de 02 (dois) fiscais por local de votação, que serão identificados pela ASU e terão o direito de acompanhar as eleições e a apuração, respeitando as determinações feitas pela Comissão Eleitoral.</p><br />

                    <p><b>Parágrafo único</b> – os locais de votação serão definidos pela Comissão Eleitoral e pelo representante de cada chapa concorrente;</p><br />

                    <p><b>Artigo 84</b> – No caso de mais de uma chapa inscrita, a cédula eleitoral obedecerá rigorosamente à ordem de inscrição protocolada.</p>
                    <p><b>Artigo 85</b> – Na cédula eleitoral para a escolha do Conselho Fiscal, a seqüência de nomes será obedecida rigorosamente à ordem de inscrição protocolada.</p>
                    <p><b>Artigo 86</b> – <span style="text-decoration:line-through;">O processo eleitoral só será legítimo se o número de votantes atingir 20% (vinte por cento) do total de associados em situação regular junto à Associação no dia das eleições.</span></p>
                    <p><span style="text-decoration:line-through;">§ 1º – Se atendidos os requisitos estabelecidos no caput deste artigo, a chapa que obtiver o maior número de votos será proclamada eleita.</span></p>
                    <p><span style="text-decoration:line-through;">§ 2º - Caso o número de votantes não atinja o quorum estabelecido no caput deste artigo, será convocada nova eleição a ser realizada em até 15 dias após o pleito.</span></p>

                    <p><span><mark> - Havendo processo eleitoral para escolha dos membros da  Diretoria Executiva e/ou conselho fiscal, será eleita aquela que obtiver a maioria simples  dos votos dos eleitores aptos a votarem. </mark></span></p>
                    <p><span><mark>§ 1º - Entende-se como eleitor apto a votar em todo e qualquer processo eleitoral ou assembleia, o associado que se encontra regular junto a entidade e que atenda o artigo 17, VI e artigo 93 do estatuto</mark></span></p>
                    <p><span><mark>§ 2º - Caso haja empate, será eleita o (a) concorrente cujo presidente tenha a sua inscrição mais antiga junto a entidade. Persistindo o empate, o concorrente mais velho.</mark></span></p>
                    <p><span><mark>§ 3º - Aplica-se os critérios do § 2º, para a eleição do Conselho Fiscal; </mark></span></p><br />

                    <p><b>Artigo 87</b> – <span style="text-decoration: line-through;"> No caso de uma única chapa concorrente, para que a mesma seja proclamada vencedora deverá: </span></p>
                        <ul style="text-decoration: line-through;">
                            <li>I-	atender ao estabelecido no artigo anterior;</li>
                            <li>II-	ter seus votos obtidos  suplantado a somatória dos votos nulos e brancos.</li>
                        </ul>
                    <p> - <mark> No caso de uma única inscrição para concorrer ás eleições,  não se realizará  o processo  eleitoral, proclamando a chapa como vencedora, dando posse aos seus membros à Diretoria Executiva ou Conselho Fiscal. </mark></p>
                    <br />

                    <p><b>Artigo 88</b> – <span style="text-decoration: line-through;"> Em caso de empate, a Diretoria Executiva da ASU marcará novas eleições após 15 (quinze) dias úteis, sendo que somente as chapas empatadas com maior número de votos concorrerão ao novo pleito, não havendo necessidade de novas inscrições.</span></p>

                    <p><mark>§ 2º - do artigo 86</mark></p>
                    <br />


                    <p><b>Artigo 89</b> – Nas apurações, serão considerados votos nulos aqueles que contiverem toda e qualquer adulteração e voto em branco quando a cédula permanecer intacta.</p>
                    <p><b>Artigo 90</b> – A Comissão Eleitoral realizará a rubrica em todas as cédulas, devendo no momento da votação manter em seu domínio a lista de assinaturas.</p>
                    <p><b>Artigo 91</b> – No momento de votar, o associado deverá apresentar qualquer documento de identificação pessoal com foto.</p>
                    <p><b>Artigo 92</b> – No processo de apuração, a Comissão Eleitoral deverá conferir minuciosamente o número de votos para que coincida com o número de assinaturas.</p>
                    <p><b>Artigo 93</b> – Somente poderão votar e serem votadas, as categorias de sócios previstas nos incisos I e II do artigo 6º que se encontrarem em situação regular junto à ASU, observando os incisos VI e VII do artigo 17.</p><br />

                    <p><b>Parágrafo único</b> – é vetado àquele (a) que mantiver vínculo empregatício com a ASU de participar de Assembleias e do processo eleitoral, seja na condição de eleitor ou de candidato. Poderá, contudo, ser convocado a auxiliar os trabalhos da Banca Eleitoral, caso haja dificuldade em reunir associados suficientes para execução dos trabalhos.</p><br />
                
                    <p><b>Artigo 94</b> – Os associados inativos da Unesp que estiverem com suas situações regularizadas junto à ASU terão direito a votar e a concorrer aos cargos eletivos da Entidade.</p>
                    <p><b>Artigo 95</b> – Durante o processo eleitoral, é dever da Diretoria Executiva que estiver em exercício promover um tratamento equânime às chapas concorrentes, fornecendo a cada uma delas, sem qualquer ônus, a relação atualizada de sócios votantes e a respectiva lotação.</p>
                    <p><b>Artigo 96</b> – Fica terminantemente proibida a toda e qualquer chapa, sob pena de responder à sindicância por infringir o presente Estatuto, a utilização da estrutura administrativa da ASU ou de meios ilícitos durante o processo eleitoral.</p>
                    <p><b>Artigo 97</b> – A posse da nova Diretoria Executiva e do Conselho Fiscal eleitos será 30 (trinta) dias após as eleições, em Assembleia Geral exclusiva para tal finalidade, devendo, na oportunidade, tanto a Diretoria Executiva como o Conselho Fiscal cessante, apresentar , respectivamente, o balancete financeiro da ASU dos últimos dois anos, aos sócios presentes;</p>
                </div>                
                <div><br />
                    <p><b>CAPÍTULO XIV</b></p>
                    <p><b>Das Disposições Gerais e Transitórias</b></p>

                    <p><b>Artigo 98</b> – Os membros da Diretoria Executiva e do Conselho Fiscal exercerão seus cargos sem qualquer remuneração, a não ser aquelas referentes a despesas com diárias e locomoções a serviço da ASU, de acordo com as normas vigentes. </p>
                    <p><b>Artigo 99</b> – A Diretoria Executiva como um todo ou um ou mais de seus membros em particular responderão, se for o caso, por danos ao patrimônio ou prejuízo causados à ASU, desde que fique caracterizada a má-fé.</p>
                    <p><b>Artigo 100</b> – O membro da Diretoria Executiva que concorrer a cargo público de natureza político–partidária deverá afastar-se de suas funções na Entidade a partir da data do registro de sua candidatura e, se for eleito, enquanto perdurar o seu mandato.</p>
                    <p><b>Artigo 101</b> – Os sócios não responderão pelas obrigações de qualquer vínculo que envolve a Entidade.</p>
                    <p><b>Artigo 102</b> – Em caso de impossibilidade financeira de se manter a Associação em atividades, a decisão da Assembleia Geral será soberana sobre os destinos de seus recursos e patrimônios porventura existentes.</p>
                    <p><b>Artigo 103</b> – Os sócios ora existentes no quadro social e que não preencham os requisitos exigidos no artigo 5º, parágrafo único, do presente Estatuto, terão seus direitos adquiridos preservados, cessando somente se o sócio modificar seu vínculo empregatício atual e não estiver legalmente vinculado às instituições mencionadas no artigo 5º.  </p>
                    <p><b>Artigo 104</b> – Subsidiariamente ao presente Estatuto, aplicam-se as leis ordinárias vigentes.</p>
                    <p><b>Artigo 105</b> – A Diretoria Executiva da ASU poderá estabelecer, sob a égide deste Estatuto, Regimentos, Normas e rotinas, visando ao adequado funcionamento da Entidade. </p>
                    <p><b>Artigo 106</b> – Com o presente Estatuto aprovado e registrado, a Diretoria Executiva deverá disponibilizá-lo na página da ASU na internet e em forma impressa na sede da Associação para consulta do associado, com o intuito de se evitar alegações de desconhecimento de seu conteúdo.</p>
                    <p><b>Artigo 107</b> – Os casos omissos no presente Estatuto serão resolvidos em Assembleia Geral, respeitando-se o quorum estabelecido nos artigos constantes no Capítulo XI, Seção I.</p>
                    <p><b>Artigo 108</b> – O presente Estatuto revoga o anterior e passa a vigorar a partir da data de sua publicação e registro.</p>
                </div><br /><br /><br />
                <style>
                    .assing p{
                        width: 100%;
                        text-align: center;
                        font-weight: 700;
                    }
                </style>
                <div class="assing">

                    <p>DR. MARCO ANTONIO COLENCI	</p>
                    <p>Assessor Jurídico da ASU – OAB/SP nº 150.163</p><br />

                    <p>DANIELA GONÇALVES</p>
                    <p>2ª Secretária da ASU</p><br />


                    <p>DJALMA SANTOS BOVOLENTA	</p>
                    <p>Presidente da ASU	</p><br />

                    <p>NEILSON CASSIMIRO DA SILVA</p>
                    <p>Presidente da Comissão de Revisão do Estatuto</p>
                </div>
                <div>
                    <br /><br />
                    <span style="background-color: yellow; width: 5px; height: 5px;"></span>
                    <p style="background-color:yellow; width: 90px; color: #bbbbba; text-align: center;">Legenda</p>
                    <p>Alterações aprovadas em assembleia, dia 12/12/2022.</p>
                </div>
            </div>

        </asp:View>
        <asp:View ID="vwRegimento" runat="server">
            <%# montarConteudo(64) %>
        </asp:View>
        <asp:View ID="vwBalancete" runat="server">
            <%# montarBalancete() %>
        </asp:View>
        <asp:View ID="vwJornal" runat="server">
            <p>Jornal</p>
            <div style="margin: 0; padding: 0"><%# montarConteudo(27) %></div>
        </asp:View>
        <asp:View ID="vwNPessoas" runat="server">
            <p>Nossas Pessoas</p>
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="lblMsgErro" runat="server"></asp:Label>
    <section id="margemRodape"></section>
</asp:Content>

