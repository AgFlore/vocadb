import EntryContract from '@DataContracts/EntryContract';
import EntryType from '@Models/EntryType';
import ContentLanguagePreference from '@Models/Globalization/ContentLanguagePreference';
import EntryRepository from '@Repositories/EntryRepository';
import EntryUrlMapper from '@Shared/EntryUrlMapper';
import ko from 'knockout';

import SearchCategoryBaseViewModel from './SearchCategoryBaseViewModel';
import SearchViewModel from './SearchViewModel';

export default class AnythingSearchViewModel extends SearchCategoryBaseViewModel<EntryContract> {
	public constructor(
		searchViewModel: SearchViewModel,
		lang: ContentLanguagePreference,
		private entryRepo: EntryRepository,
	) {
		super(searchViewModel);

		this.loadResults = (
			pagingProperties,
			searchTerm,
			tags,
			childTags,
			status,
			callback,
		): Promise<void> =>
			this.entryRepo
				.getList(
					pagingProperties,
					lang,
					searchTerm,
					tags,
					childTags,
					this.fields(),
					status,
				)
				.then(callback);
	}

	public entryCategoryName = (entry: EntryContract): string => {
		switch (EntryType[entry.entryType as keyof typeof EntryType]) {
			case EntryType.Artist:
				return this.searchViewModel.resources().artistTypeNames![
					entry.artistType!
				];
			case EntryType.Album:
				return this.searchViewModel.resources().discTypeNames![entry.discType!];
			case EntryType.ReleaseEvent:
				return this.searchViewModel.resources().eventCategoryNames![
					entry.eventCategory!
				];
			case EntryType.Song:
				return this.searchViewModel.resources().songTypeNames![entry.songType!];
		}

		return null!;
	};

	public entryUrl = (entry: EntryContract): string => {
		return EntryUrlMapper.details(entry.entryType, entry.id);
	};

	public fields = ko.computed(() =>
		this.searchViewModel.showTags()
			? 'AdditionalNames,MainPicture,Tags'
			: 'AdditionalNames,MainPicture',
	);
}
