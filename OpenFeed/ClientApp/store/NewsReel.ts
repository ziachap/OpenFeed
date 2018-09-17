import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface NewsReelState {
    isLoading: boolean;
	paginatedArticles: IPaginatedArticles;
	searchConfiguration: NewsSearchConfiguration;
}

// TODO: These types that map to a C# type should probably go in one place (e.g. /ApiModels) ?
export interface Article {
    title: string;
    description: string;
    url: string;
    imageUrl: string;
    publishDate: string;
    author: string;
	source: string;
	category: string;
}

// TODO: Move this to relevant place
export class NewsSearchConfiguration {
    categoryId?: number;
    page?: number;
}

export interface IPaginatedArticles {
	results: Article[];
	page: number;
	pageSize: number;
	totalPages: number;
	hasNextPage: boolean;
	hasPreviousPage: boolean;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestArticlesAction {
    type: 'REQUEST_ARTICLES';
}

interface ReceiveArticlesAction {
    type: 'RECEIVE_ARTICLES';
	results: IPaginatedArticles;
}

interface SetCategoryAction {
	type: 'SET_CATEGORY';
	categoryId?: number;
}

interface SetPageAction {
	type: 'SET_PAGE';
	page: number;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestArticlesAction | ReceiveArticlesAction | SetCategoryAction | SetPageAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    // TODO: Perhaps move this out to an INewsService so it can be reused
	requestArticles: (newsSearchConfig: NewsSearchConfiguration): AppThunkAction<KnownAction> => (dispatch, getState) => {
		
        // TODO: Move this URL to some special place with all the api URLs 
		var url = "/newsapi?" + makeQueryString(newsSearchConfig);

        let fetchTask = fetch(url)
			.then(response => response.json() as Promise<IPaginatedArticles>)
			.then(data => {
				dispatch({ type: 'RECEIVE_ARTICLES', results: data });
            });

        addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
        dispatch({ type: 'REQUEST_ARTICLES' });

        // TODO: Perhaps move this out somewhere? There is probably a nicer way to do this
        function makeQueryString(controls: NewsSearchConfiguration) : string {
            var str = "";
			if (defined(controls.categoryId)) str += "categoryId=" + controls.categoryId;
			if (defined(controls.page)) str += "&page=" + controls.page;
			return str;

			function defined(value:any):boolean {
				return value !== undefined && value !== null;
			}
        }
	},
	setCategory: (categoryId?: number): AppThunkAction<SetCategoryAction> => (dispatch, getState) => {
		dispatch({ type: 'SET_CATEGORY', categoryId: categoryId });
	},
	setPage: (page: number): AppThunkAction<SetPageAction> => (dispatch, getState) => {
		dispatch({ type: 'SET_PAGE', page: page });
	}
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedArticles: IPaginatedArticles = {
	results: [],
	page: 0,
	pageSize: 0,
	totalPages: 0,
	hasNextPage: false,
	hasPreviousPage: false
};

const unloadedState: NewsReelState = {
	paginatedArticles: unloadedArticles , isLoading: false, searchConfiguration: new NewsSearchConfiguration()
};

export const reducer: Reducer<NewsReelState> = (state: NewsReelState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_ARTICLES':
            return {
	            paginatedArticles: state.paginatedArticles,
				isLoading: true,
                searchConfiguration: state.searchConfiguration
            };
        case 'RECEIVE_ARTICLES':
            return {
	            paginatedArticles: action.results,
				isLoading: false,
                searchConfiguration: state.searchConfiguration
			};
		case 'SET_CATEGORY':
			var newConfig = state.searchConfiguration;
			newConfig.categoryId = action.categoryId;
			newConfig.page = 0;
	        return {
		        paginatedArticles: state.paginatedArticles,
		        isLoading: state.isLoading,
				searchConfiguration: newConfig
			};
        case 'SET_PAGE':
	        var newConfig = state.searchConfiguration;
			newConfig.page = action.page;
	        return {
		        paginatedArticles: state.paginatedArticles,
		        isLoading: state.isLoading,
		        searchConfiguration: newConfig
	        };
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

	return state || unloadedState;
};
