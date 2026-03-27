namespace WebTemplate.Services{
    public class SesijaService:ISesijaService
{
    public ISesijaRepository SesijaRepo { get; }
    public IDeoRepository DeoRepo { get; }

    public SesijaService(ISesijaRepository sesijaRepo, IDeoRepository deoRepo)
    {
        SesijaRepo = sesijaRepo;
        DeoRepo = deoRepo;
    }

    public async Task<bool> DodajSesijuAsync(int deoId, SesijaDTO dto)
    {
        
        var deo = await DeoRepo.DbSet
            .Include(d => d.Sesije)
            .FirstOrDefaultAsync(d => d.Id == deoId);

        if (deo == null || deo.Tip != TipPodeoka.sesija)
            return false;

        // Pravilo: Jedan slot - jedna sesija
        if (deo.Sesije != null && deo.Sesije.Any())
            throw new Exception("Ovaj termin je već zauzet.");

     
        var sesija = Mapper.Map(dto);
        sesija.Deo = deo;

        await SesijaRepo.AddAsync(sesija);
        await SesijaRepo.SaveChangesAsync();
        return true;
    }

    public async Task<SesijaDTO?> PreuzmiDetaljeSesijeAsync(int id)
    {
        var sesija = await SesijaRepo.GetSesijaSaPredavacem(id);
        return sesija == null ? null : Mapper.MapToDto(sesija);//poseban dto iz modela u front
    }

    public async Task ObrisiSesiju(int id)
    {
        var sesija = await SesijaRepo.GetByIdAsync(id);
        if (sesija != null)
        {
            SesijaRepo.Delete(sesija);
            await SesijaRepo.SaveChangesAsync();
        }
    }
}}