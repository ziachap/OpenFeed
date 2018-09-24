import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { connect } from "react-redux";
import * as NewsReelState from "../store/NewsReel";
import { ApplicationState } from "../store";
import ArticleCard from "./ArticleCard";
import Pagination from "./Pagination";
import Categories from "./Categories";
import Sort from "./Sort";
import SearchInput from "./SearchInput";
import { newsCategoryService } from "../models/NewsCategories";
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

	private breadcrumb(): string {
		let breadcrumb = `United Kingdom > ${newsCategoryService.getName(this.props.searchConfiguration.categoryId)}`;
		if (defined(this.props.searchConfiguration.text)) {
			breadcrumb += ` > "${this.props.searchConfiguration.text}"`;
		}
		return breadcrumb;

		function defined(value?: string): boolean {
			return value !== undefined && value !== null && value !== "";
		}
	}

	render() {
		return <div className="ui container text">
					<div className="ui tiny thin serif header">
						{this.breadcrumb()}
			       </div>
			       <p></p>
			       <Pagination {...this.props}/>
			       <div className="ui very padded segment">
				       <div className="ui right rail">
					       {this.renderMenu()}
				       </div>

				       {this.props.isLoading ? this.renderLoader() : this.renderArticles()}
			       </div>
			       <Pagination {...this.props}/>
		       </div>;
	}

	private renderLoader() {
		return <div className="ui active inverted dimmer">
			       <div className="ui text loader">Loading</div>
		       </div>;
	}

	private renderArticles() {
		return <div className="ui divided items">
			       {this.props.paginatedArticles.totalPages === 0 ? <h4>No articles found</h4> : ""}
			       {this.props.paginatedArticles.results.map(
				       article => <ArticleCard {...article} key={article.title}/>)}
		       </div>;
	}

	private renderMenu() {
		return <div className="ui">
			       <div className="ui vertical text small menu">
				       <SearchInput {...this.props}/>
				       <Sort {...this.props}/>
				       <Categories {...this.props}/>
			       </div>
		       </div>;
	}

	private renderFrame() {
		return <div className="ui vertical text medium menu">
			       <iframe name="iframe_article"></iframe>
		       </div>;
	}
}

/*
<div className="header item">Other</div>
<div className="ui small form">
   <div className="inline field">
       <div className="ui checkbox">
	       <input type="checkbox" className="hidden"/>
	       <label>Business</label>
       </div>
   </div>
</div>
 */

export default connect(
	(state: ApplicationState) => state.newsReel, // Selects which state properties are merged into the component's props
	NewsReelState.actionCreators // Selects which action creators are merged into the component's props
)(NewsReel) as typeof NewsReel