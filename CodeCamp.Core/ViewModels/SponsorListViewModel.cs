using System;
using System.Linq;
using System.Collections.Generic;
using CodeCamp.Core.Entities;

namespace CodeCamp.Core.ViewModels
{
	public class SponsorListViewModel
	{
		public IList<Tier> Tiers { get; set; }
		
		public SponsorListViewModel(IList<string> tiers, IList<Sponsor> sponsors)
		{
			Tiers = new List<Tier>();
			
			foreach (string tier in tiers)
			{
				var sponsorsInTier = 
					sponsors
						.Where(sponsor => sponsor.Tier.ToLower() == tier.ToLower())
						.OrderBy(sponsor => sponsor.Name)
						.ToList();
				
				if (sponsorsInTier.Count > 0)
				{
					Tiers.Add(new Tier
					{
						Name = tier,
						Sponsors = sponsorsInTier
					});
				}
			}
		}
		
		public class Tier
		{
			public string Name { get; set; }
			public IList<Sponsor> Sponsors { get; set; }
		}
	}
}

