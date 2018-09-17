class NewsCategory {
	constructor(name: string, id: number) {
		this.name = name;
		this.id = id;
	}

	name: string;
	id: number;
}

export class NewsCategoryService {
	constructor() {
	}

	getName(id?: number): string {
		var category = newsCategories.find(c => c.id === id);
		if (category === undefined || category === null) {
			return "All";
		}
		return category.name;
	}
}

export var newsCategories = [
	new NewsCategory("Business", 0),
	new NewsCategory("Entertainment", 1),
	new NewsCategory("Health", 2),
	new NewsCategory("Science", 3),
	new NewsCategory("Sports", 4),
	new NewsCategory("Technology", 5)
];

export var newsCategoryService = new NewsCategoryService();