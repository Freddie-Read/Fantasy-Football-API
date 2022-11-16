using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Collections.ObjectModel;

// URL FOR API: https://medium.com/@frenzelts/fantasy-premier-league-api-endpoints-a-detailed-guide-acbd5598eb19

namespace Fantasy_Football_API
{
    public partial class Form1 : Form
    {
        PlayerCollection playerCollection = new PlayerCollection();
        public Form1()
        {
            InitializeComponent();
        }

        private void callAPIBtn_Click(object sender, EventArgs e)
        {
            callAPI();
        }

        private void callAPI()
        {
            string url = string.Format("https://fantasy.premierleague.com/api/bootstrap-static/");
            WebRequest requestObject = WebRequest.Create(url);
            requestObject.Method = "GET";
            HttpWebResponse responseObject = null;
            responseObject = (HttpWebResponse)requestObject.GetResponse();

            string strResult = "";
            using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strResult = sr.ReadToEnd();
                sr.Close();
            }

            dynamic jsonObject = JObject.Parse(strResult);

            var playersJson = jsonObject["elements"];

            List<Player> players = new List<Player>();

            foreach (var player in playersJson)
            {
                int length = 16;
                string[] data = new string[length];
                data[0] = player["first_name"];
                data[1] = player["second_name"];
                data[2] = player["id"];
                data[3] = player["team"];
                data[4] = player["total_points"];
                data[5] = player["form"];
                data[6] = player["now_cost"];
                data[7] = player["selected_by_percent"];
                data[8] = player["minutes"];
                data[9] = player["goals_scored"];
                data[10] = player["assists"];
                data[11] = player["clean_sheets"];
                data[12] = player["bonus"];
                data[13] = player["yellow_cards"];
                data[14] = player["red_cards"];
                data[15] = player["element_type"];

                players.Add(new Player(data));
            }

            playerCollection.playerList = players;
        }

        private void sortBtn_Click(object sender, EventArgs e)
        {
            createSquad();
            //createBestSquad();
            //TextBox sortTermTxt = sortTxt;
            //TextBox displayText = displayTxt;

            //displayText.Text = "";
            //string sortTerm = sortTermTxt.Text;

            //sortTerm = char.ToUpper(sortTerm[0]) + sortTerm.Substring(1).ToLower();

            //SortingTerms term;

            //if (!Enum.TryParse(sortTerm, out term))
            //{
            //    displayText.Text = "Cannot sort by this value";
            //    return;
            //}

            //PlayerCollection sortedPlayers = playerCollection.sortPlayers(term);
            //createBestSquad(sortedPlayers);
            ////createSquad(sortedPlayers, term);

            //return;
        }

        private void createSquad()
        {
            int[] currLineup = { 0, 0, 0, 0 };
            int[] fourThreeThree = { 1, 4, 3, 3 };
            int[] lineup = { 2, 5, 5, 3 };
            int poolSize = 60;

            SortingTerms[] terms = { SortingTerms.TotalPoints, SortingTerms.Form/*, SortingTerms.Bonus*/ };
            PlayerCollection playerList = playerCollection;

            Dictionary<Player, int[]> frequency = new Dictionary<Player, int[]>();

            TextBox displayErrors = suggestionTxt;
            displayErrors.Text = "";
            var newLine = Environment.NewLine;

            TextBox displayText = displayTxt;
            displayText.Text = "";

            playerList.sortPlayers(SortingTerms.TotalPoints);
            foreach (var player in playerList.getPlayerList())
            {
                displayText.Text += $"{player.getTotalPoints},{player.getForm}{newLine}";
            }
            return;
            //foreach (var term in terms)
            //{
            //    playerList.sortPlayers(term);
            //    for (int j = 0; j < poolSize; j++)
            //    {
            //        if (frequency.ContainsKey(playerList.getPlayer(j)))
            //        {
            //            frequency[playerList.getPlayer(j)][0] += 1;
            //            frequency[playerList.getPlayer(j)][1] += j;
            //        }
            //        else
            //        {
            //            frequency[playerList.getPlayer(j)] = new int[] { 1, j }; //{ frequency, rank }
            //        }
            //    }
            //}

            //Dictionary<Player, int> rankedDictionary = new Dictionary<Player, int>();

            //foreach (KeyValuePair<Player, int[]> element in frequency)
            //{
            //    if (element.Value[0] == terms.Length)
            //    {
            //        rankedDictionary[element.Key] = element.Value[1];
            //    }
            //}

            //List<Player> unsortedPlayerList = rankedDictionary.Keys.ToList();

            //for (int i = 0; i < unsortedPlayerList.Count - 2; i++)
            //{
            //    for (int j = 0; j < unsortedPlayerList.Count - 1 - i; j++)
            //    {
            //        if (rankedDictionary[unsortedPlayerList[j]] > rankedDictionary[unsortedPlayerList[j + 1]])
            //        {
            //            Player temp = unsortedPlayerList[j];
            //            unsortedPlayerList[j] = unsortedPlayerList[j + 1];
            //            unsortedPlayerList[j + 1] = temp;
            //        }
            //    }
            //}

            //TextBox displayText = displayTxt;
            //displayText.Text = "";
            ////int index = 0;

            ////foreach (var player in unsortedPlayerList)
            ////{
            ////    index++;
            ////    displayText.Text += $"({index}) {player.getFirstName} {player.getLastName}: {rankedDictionary[player]}{newLine}";
            ////}

            //float totalCost = 0.0f;
            //int index = 0;
            //foreach (var player in unsortedPlayerList)
            //{
            //    int postion = (int)player.getPosition - 1;
            //    if (currLineup[postion] == lineup[postion])
            //    {
            //        continue;
            //    }

            //    totalCost += player.getCost / 10;
            //    if (totalCost > 100)
            //    {
            //        totalCost -= player.getCost / 10;
            //        continue;
            //    }

            //    displayText.Text += $"({index}) {player.getFirstName} {player.getLastName}: £{player.getCost / 10}" +
            //        $" - ({player.getPosition}){newLine}";
            //    currLineup[postion]++;
            //    index++;
            //}

            //displayText.Text += $"Total Cost: £{totalCost}";
        }

        private void createBestSquad()
        {
            PlayerCollection sortedPlayers = playerCollection;
            SortingTerms[] terms = { SortingTerms.TotalPoints, SortingTerms.Form};

            int termAmount = terms.Length;

            int[] lineup = { 2, 5, 5, 3 };
            int poolSize = 50;

            Dictionary<Player, int> eligiblePlayers = new Dictionary<Player, int>();
            for (int i = 0; i < terms.Length; i++)
            {
                sortedPlayers.sortPlayers(terms[i]);
                for (int j = 0; j < poolSize; j++)
                {
                    if (eligiblePlayers.ContainsKey(sortedPlayers.getPlayer(j)))
                    {
                        eligiblePlayers[sortedPlayers.getPlayer(j)]++;
                    }
                    else
                    {
                        eligiblePlayers[sortedPlayers.getPlayer(j)] = 1;
                    }
                }
            }

            List<Player> eligiblePlayersList = new List<Player>();

            foreach (KeyValuePair<Player, int> element in eligiblePlayers)
            {
                if (element.Value == terms.Length)
                {
                    eligiblePlayersList.Add(element.Key);
                }
            }

            PlayerCollection playerShortlist = new PlayerCollection(eligiblePlayersList);

            Dictionary<Player, int> validPlayers2 = new Dictionary<Player, int>();
            for (int i = 0; i < poolSize; i++)
            {
                if (!validPlayers2.ContainsKey(playerShortlist.getPlayer(i)))
                {
                    foreach (var player in playerShortlist.getPlayerList())
                    {

                        //validPlayers2[player] = ;
                    }
                }
            }
        }

        private void sortFirstBtn_Click(object sender, EventArgs e)
        {
            TextBox sortTermTxt = sortTxt;
            TextBox displayText = displayTxt;

            int[] fourFourTwo = { 1, 4, 4, 2 };
            int[] fourThreeThree = { 1, 4, 3, 3 };
            int[] threeFourThree = { 1, 4, 4, 2 };

            displayText.Text = "";
            string sortTerm = sortTermTxt.Text;

            sortTerm = char.ToUpper(sortTerm[0]) + sortTerm.Substring(1).ToLower();

            SortingTerms term;

            if (!Enum.TryParse(sortTerm, out term))
            {
                displayText.Text = "Cannot sort by this value";
                return;
            }

            PlayerCollection sortedPlayers = playerCollection.sortPlayers(term);
            firstTeamOnly(sortedPlayers, fourThreeThree, term);

            return;
        }

        /*private void createSquad(PlayerCollection sortedPlayers, SortingTerms term)
        {
            TextBox displayText = displayTxt;
            int[] lineup = { 2, 5, 5, 3 };
            int[] currLineup = { 0, 0, 0, 0 };

            float totalCost = 0.0f;

            foreach (var player in sortedPlayers.playerList)
            {
                int postion = (int)player.getPosition - 1;
                if (currLineup[postion] == lineup[postion])
                {
                    continue;
                }

                totalCost += player.getCost;

                displayText.Text += $"[{Environment.NewLine}\tName: {player.getFirstName} {player.getLastName} ({player.getPosition})" +
                    $"{Environment.NewLine}\tPlays For: {player.getTeam}{Environment.NewLine}";

                switch ((int)term)
                {
                    case 0:
                        displayText.Text += $"\tTotal Points: {player.getTotalPoints}{Environment.NewLine}]";
                        break;
                    case 1:
                        displayText.Text += $"\tForm: {player.getForm}{Environment.NewLine}]";
                        break;
                    case 2:
                        displayText.Text += $"\tCost: {player.getCost}{Environment.NewLine}]";
                        break;
                    case 3:
                        displayText.Text += $"\tSelected Percent (%): {player.getSelectedPercent}{Environment.NewLine}]";
                        break;
                    case 4:
                        displayText.Text += $"\tTotal Minutes: {player.getMinutes}{Environment.NewLine}]";
                        break;
                    case 5:
                        displayText.Text += $"\tGoals: {player.getGoals}{Environment.NewLine}]";
                        break;
                    case 6:
                        displayText.Text += $"\tAssists: {player.getAssists}{Environment.NewLine}]";
                        break;
                    case 7:
                        displayText.Text += $"\tClean Sheets: {player.getCleanSheets}{Environment.NewLine}]";
                        break;
                    case 8:
                        displayText.Text += $"\tBonus Points: {player.getBonus}{Environment.NewLine}]";
                        break;
                    case 9:
                        displayText.Text += $"\tYellow Cards: {player.getYellowCards}{Environment.NewLine}]";
                        break;
                    case 10:
                        displayText.Text += $"\tRed Cards: {player.getRedCards}{Environment.NewLine}]";
                        break;
                    default:
                        displayText.Text += $"\tTotal Points: {player.getTotalPoints}{Environment.NewLine}]";
                        break;
                }

                displayText.Text += Environment.NewLine;

                currLineup[postion]++;
            }

            displayText.Text += $"{Environment.NewLine}Team Cost: £{totalCost / 10}";
        }*/

        private void firstTeamOnly(PlayerCollection sortedPlayers, int[] formation, SortingTerms term)
        {
            TextBox displayText = displayTxt;
            int[] currLineup = { 0, 0, 0, 0 };
            int[] fourThreeThree = { 1, 4, 3, 3 };
            TextBox suggestionText = suggestionTxt;

            float totalCost = 0.0f;

            foreach (var player in sortedPlayers.playerList)
            {
                int postion = (int)player.getPosition - 1;
                if (currLineup[postion] == fourThreeThree[postion])
                {
                    suggestionText.Text += $"Rejected: {player.getLastName}{Environment.NewLine}";
                    continue;
                }

                totalCost += player.getCost;

                displayText.Text += $"[{Environment.NewLine}\tName: {player.getFirstName} {player.getLastName} ({player.getPosition})" +
                    $"{Environment.NewLine}\tPlays For: {player.getTeam}{Environment.NewLine}";

                switch ((int)term)
                {
                    case 0:
                        displayText.Text += $"\tTotal Points: {player.getTotalPoints}{Environment.NewLine}]";
                        break;
                    case 1:
                        displayText.Text += $"\tForm: {player.getForm}{Environment.NewLine}]";
                        break;
                    case 2:
                        displayText.Text += $"\tCost: {player.getCost}{Environment.NewLine}]";
                        break;
                    case 3:
                        displayText.Text += $"\tSelected Percent (%): {player.getSelectedPercent}{Environment.NewLine}]";
                        break;
                    case 4:
                        displayText.Text += $"\tTotal Minutes: {player.getMinutes}{Environment.NewLine}]";
                        break;
                    case 5:
                        displayText.Text += $"\tGoals: {player.getGoals}{Environment.NewLine}]";
                        break;
                    case 6:
                        displayText.Text += $"\tAssists: {player.getAssists}{Environment.NewLine}]";
                        break;
                    case 7:
                        displayText.Text += $"\tClean Sheets: {player.getCleanSheets}{Environment.NewLine}]";
                        break;
                    case 8:
                        displayText.Text += $"\tBonus Points: {player.getBonus}{Environment.NewLine}]";
                        break;
                    case 9:
                        displayText.Text += $"\tYellow Cards: {player.getYellowCards}{Environment.NewLine}]";
                        break;
                    case 10:
                        displayText.Text += $"\tRed Cards: {player.getRedCards}{Environment.NewLine}]";
                        break;
                    default:
                        displayText.Text += $"\tTotal Points: {player.getTotalPoints}{Environment.NewLine}]";
                        break;
                }

                displayText.Text += Environment.NewLine;

                currLineup[postion]++;
            }

            displayText.Text += $"{Environment.NewLine}Team Cost: £{totalCost / 10}";
        }

    }

    public enum Team
    {
        Arsenal = 1,
        Aston_Villa = 2,
        Bournemouth = 3,
        Brentford = 4,
        Brighton_And_Hove_Albion = 5,
        Chelsea = 6,
        Crystal_Palace = 7,
        Everton = 8,
        Fulham = 9,
        Leicester_City = 10,
        Leeds_United = 11,
        Liverpool = 12,
        Manchester_City = 13,
        Manchester_United = 14,
        Newcastle_United = 15,
        Nottingham_Forest = 16,
        Southampton = 17,
        Tottenham_Hotspur = 18,
        West_Ham_United = 19,
        Wolverhampton_Wanderers = 20
    }

    public enum SortingTerms
    {
        TotalPoints = 0,
        Form = 1,
        Cost = 2,
        SelectedPercent = 3,
        Minutes = 4,
        Goals = 5,
        Assists = 6,
        CleanSheets = 7,
        Bonus = 8,
        YellowCards = 9,
        RedCards = 10
    }

    public enum Position
    {
        Goalkeeper = 1,
        Defender = 2,
        Midfielder = 3,
        Forward = 4
    }

    public class PlayerCollection
    {
        public List<Player> playerList;

        public PlayerCollection(){}

        public PlayerCollection(List<Player> playerList)
        {
            this.playerList = playerList;
        }

        public PlayerCollection sortPlayers(SortingTerms sortTerm)
        {
            PlayerCollection sortedCollection = new PlayerCollection(playerList);
            int sortValue = (int)sortTerm;

            for (int i = 0; i < sortedCollection.playerList.Count - 2; i++)
            {
                for (int j = 0; j < sortedCollection.playerList.Count - 1 - i; j++)
                {
                    Player player1 = sortedCollection.getPlayer(j);
                    Player player2 = sortedCollection.getPlayer(j + 1);

                    bool swap;

                    switch (sortValue)
                    {
                        case 0:
                            swap = !player1.compare(player1.getTotalPoints, player2.getTotalPoints);
                            break;
                        case 1:
                            swap = !player1.compare(player1.getForm, player2.getForm);
                            break;
                        case 2:
                            swap = !player1.compare(player1.getCost, player2.getCost);
                            break;
                        case 3:
                            swap = !player1.compare(player1.getSelectedPercent, player2.getSelectedPercent);
                            break;
                        case 4:
                            swap = !player1.compare(player1.getMinutes, player2.getMinutes);
                            break;
                        case 5:
                            swap = !player1.compare(player1.getGoals, player2.getGoals);
                            break;
                        case 6:
                            swap = !player1.compare(player1.getAssists, player2.getAssists);
                            break;
                        case 7:
                            swap = !player1.compare(player1.getCleanSheets, player2.getCleanSheets);
                            break;
                        case 8:
                            swap = !player1.compare(player1.getBonus, player2.getBonus);
                            break;
                        case 9:
                            swap = !player1.compare(player1.getYellowCards, player2.getYellowCards);
                            break;
                        case 10:
                            swap = !player1.compare(player1.getRedCards, player2.getRedCards);
                            break;
                        default:
                            swap = !player1.compare(player1.getTotalPoints, player2.getTotalPoints);
                            break;
                    }

                    if (swap)
                    {
                        Player temp = sortedCollection.getPlayer(j);
                        sortedCollection.playerList[j] = sortedCollection.playerList[j + 1];
                        sortedCollection.playerList[j + 1] = temp;
                    }
                }
            }

            return sortedCollection;
        }

        public Player getPlayer(int index)
        {
            return playerList[index];
        }

        public List<Player> getPlayerList()
        {
            return playerList;
        }

        public int getPlayerCount()
        {
            return playerList.Count;
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerCollection collection &&
                   EqualityComparer<List<Player>>.Default.Equals(playerList, collection.playerList) &&
                   EqualityComparer<List<Player>>.Default.Equals(playerList, collection.playerList);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Player
    {
        private string firstName;
        private string lastName;
        private int id;
        private Team team;
        private int totalPoints;
        private float form;
        private float cost;
        private float selectedPercent;
        private int minutes;
        private int goals;
        private int assists;
        private int cleanSheets;
        private int bonus;
        private int yellowCards;
        private int redCards;
        private Position position;

        public Player(string[] data)
        {
            firstName = data[0];
            lastName = data[1];
            id = int.Parse(data[2]);
            team = (Team)int.Parse(data[3]);
            totalPoints = int.Parse(data[4]);
            form = float.Parse(data[5]);
            cost = float.Parse(data[6]);
            selectedPercent = float.Parse(data[7]);
            minutes = int.Parse(data[8]);
            goals = int.Parse(data[9]);
            assists = int.Parse(data[10]);
            cleanSheets = int.Parse(data[11]);
            bonus = int.Parse(data[12]);
            yellowCards = int.Parse(data[13]);
            redCards = int.Parse(data[14]);
            position = (Position)int.Parse(data[15]);
        }

        // If val2 is larger, return false, else, return true (true if same)
        public bool compare(float val1, float val2)
        {
            if (val2 > val1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string getFirstName { get => firstName; }
        public string getLastName { get => lastName; }
        public int getId { get => id; }
        public Team getTeam { get => team; }
        public int getTotalPoints { get => totalPoints; }
        public float getForm { get => form; }
        public float getCost { get => cost; }
        public float getSelectedPercent { get => selectedPercent; }
        public int getMinutes { get => minutes; }
        public int getGoals { get => goals; }
        public int getAssists { get => assists; }
        public int getCleanSheets { get => cleanSheets; }
        public int getBonus { get => bonus; }
        public int getYellowCards { get => yellowCards; }
        public int getRedCards { get => redCards; }
        public Position getPosition { get => position; }

    }
}
