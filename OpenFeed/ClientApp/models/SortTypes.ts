class SortType {
	constructor(name: string, id: number) {
		this.name = name;
		this.id = id;
	}

	name: string;
	id: number;
}

export class SortTypesService {
	getName(id: number): string {
		var sortType = sortTypes.find(c => c.id === id);
		if (sortType === undefined || sortType === null) {
			return "N/A";
		}
		return sortType.name;
	}
}

export var sortTypes = [
	new SortType("Date Published (Newest)", 0),
	new SortType("Date Published (Oldest)", 1)
];

export var sortTypesService = new SortTypesService();