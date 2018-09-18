import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { connect } from 'react-redux';
import * as NewsReelState from "../store/NewsReel";
import { ApplicationState } from '../store';
import ArticleCard from "./ArticleCard";
import Pagination from "./Pagination";
import Categories from "./Categories";
import { newsCategories, newsCategoryService} from "../models/NewsCategories";
import "jquery";

export type NewsReelProps = NewsReelState.NewsReelState
    & typeof NewsReelState.actionCreators
    & RouteComponentProps<{}>;

class NewsReel extends React.Component<NewsReelProps, {}> {

	constructor(props: NewsReelProps) {
		super(props);
	}

    componentWillMount() {
	    this.props.requestArticles();
    }

    public render() {
        return <div className="ui container text">

                   <div className="ui very padded segment">
                       <div className="ui right rail">
                           {this.renderMenu()}
                       </div>
				<h1>United Kingdom > {newsCategoryService.getName(this.props.searchConfiguration.categoryId)}</h1>
				<p></p>
				<Pagination {...this.props}/>
				{this.props.isLoading ? <span>Loading...</span> : this.renderArticles()}
	            <Pagination {...this.props} />
                   </div>
               </div>;
	}

    private renderArticles() {
        return <div className="ui divided items">
			{this.props.paginatedArticles.results.map(article => <ArticleCard {...article} key={article.title}/>)}
               </div>;
	}

	private renderMenu() {
        return <div className="ui">
			<div className="ui vertical text small menu">

                <div className="header item">Sort By</div>
                <a className="item">Most Relevant</a>
                <a className="item">Most Popular</a>
				<a className="active item">Newest</a>
				
				<Categories {...this.props} />

                <div className="header item">Other</div>
                <div className="ui small form">
                    <div className="inline field">
                        <div className="ui checkbox">
                            <input type="checkbox" className="hidden" />
                            <label>Business</label>
                        </div>
                    </div>
                </div>
               </div>
           </div>;
	}
}


export default connect(
(state: ApplicationState) => state.newsReel, // Selects which state properties are merged into the component's props
NewsReelState.actionCreators // Selects which action creators are merged into the component's props
)(NewsReel) as typeof NewsReel

