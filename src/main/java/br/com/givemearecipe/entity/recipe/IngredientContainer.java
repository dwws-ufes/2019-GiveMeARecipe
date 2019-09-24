package br.com.givemearecipe.entity.recipe;

import java.util.*;
import java.math.*;
import javax.persistence.*;
import javax.validation.constraints.*;
import br.ufes.inf.nemo.jbutler.ejb.persistence.PersistentObjectSupport;

/** TODO: generated by FrameWeb. Should be documented. */
@Entity
public class IngredientContainer extends PersistentObjectSupport implements Comparable<IngredientContainer> {
	/** Serialization id. */
	private static final long serialVersionUID = 1L;



	/** TODO: generated by FrameWeb. Should be documented. */
	@NotNull
	private String ingredient_condition;




		
		/** TODO: generated by FrameWeb. Should be documented. */
		@ManyToOne
		private Ingredient Target;
		
	

		
		/** TODO: generated by FrameWeb. Should be documented. */
		@OneToOne
		private Portion Target;
		
	

		
		/** TODO: generated by FrameWeb. Should be documented. */
		@ManyToOne
		private Recipe Source;
		
	




	/** Getter for ingredient_condition. */
	public String getIngredient_condition() {
		return ingredient_condition;
	}
	
	/** Setter for ingredient_condition. */
	public void setIngredient_condition(String ingredient_condition) {
		this.ingredient_condition = ingredient_condition;
	}




		
		/** Getter for Target. */
		public Ingredient getTarget() {
			return Target;
		}
		
		/** Setter for Target. */
		public void setTarget(Ingredient Target) {
			this.Target = Target;
		}
		
	

		
		/** Getter for Target. */
		public Portion getTarget() {
			return Target;
		}
		
		/** Setter for Target. */
		public void setTarget(Portion Target) {
			this.Target = Target;
		}
		
	

		
		/** Getter for Source. */
		public Recipe getSource() {
			return Source;
		}
		
		/** Setter for Source. */
		public void setSource(Recipe Source) {
			this.Source = Source;
		}
		
	





	/** @see java.lang.Comparable#compareTo(java.lang.Object) */
	@Override
	public int compareTo(IngredientContainer o) {
		// FIXME: auto-generated method stub		
		return super.compareTo(o);
	}
}