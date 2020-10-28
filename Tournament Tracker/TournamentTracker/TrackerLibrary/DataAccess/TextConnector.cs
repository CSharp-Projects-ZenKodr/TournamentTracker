using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TrackerLibrary.DataAccess.TextHelpers;

using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess {
    //TODO - Wire up the CreatePrize for text files.
    public class TextConnector : IDataConnection {
        public void CompleteTournament(TournamentModel model) {
            List<TournamentModel> tournaments = GlobalConfig.TournamentsFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels();
            
            tournaments.Remove(model);

            tournaments.SaveToTournamentFile();

            TournamentLogic.UpdateTournamentResults(model);
        }

        public void CreatePerson(PersonModel model) {
            List<PersonModel> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if (people.Count > 0) {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            people.Add(model);

            people.SaveToPeopleFile();
        }

        public void CreatePrize(PrizeModel model) {
            //*Load Text File

            //*Covert Text to List<PrizeMode>
            
            //*Find the max ID
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            int currentId = 1;

            if (prizes.Count > 0) {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            //*Add The new record with new ID(max+1)
            prizes.Add(model);

            //*convert the prizes to List<string>

            //*Save List<string> to the text file
            prizes.SaveToPrizeFile();
        }

        public void CreateTeam(TeamModel model) {
            List<TeamModel> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels();

            int currentId = 1;

            if (teams.Count > 0) {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            teams.Add(model);

            teams.SaveToTeamFile();
        }

        public void CreateTournament(TournamentModel model) {
            List<TournamentModel> tournaments = GlobalConfig.TournamentsFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels();
            int currentId = 1;

            if (tournaments.Count > 0) {
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            model.SaveRoundsToFile();

            tournaments.Add(model);

            tournaments.SaveToTournamentFile();

            TournamentLogic.UpdateTournamentResults(model);
        }

        public List<PersonModel> GetPerson_All() {
            return GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeam_All() {
            return GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels();
        }

        public List<TournamentModel> GetTournament_All() {
            return GlobalConfig.TournamentsFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels();
        }

        public void UpdateMatchup(MatchupModel model) {
            model.UpdateMatchupToFile();
        }
    }
}