namespace PokemonAPI.Repositories
{
    public interface IPokemonRepository
    {
        string[] GetPokemonsWithSimilarName(string name, int take = 3, int levenshteinDistance = 3);
    }
}
